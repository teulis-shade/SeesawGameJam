using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor.Animations;

public class PlayerScript : MonoBehaviour
{
    [Header("Configurable Values")]
    [Range(0f, 100000f)]
    public double mass = 100f; // kg
    [Range(-40f, 40f)]
    public float gravity = 9.81f; // m / s^2
    [Range(0.01f, 10f)]
    public float referenceArea = 0.140f; // m^2
    [Range(0f, 2f)]
    public float dragCoefficient = 0.7f;
    [Range(0.10f, 0.50f)]
    public float dragPower = 0.5f;
    [SerializeField] private bool useCustomDensity = false;
    [Range(0.01f, 1000f)]
    public float customAirDensity = 1.21f; // kg / m^3
    [Range(2000, 70000)]
    public int planetRadius = 20000; // m
    [Range(-5f, 10f)]
    public float groundLevel = 2f; // m
    [Range(0f, 10f)]
    public float minimumImpulse = 5f; // m

    [Header("Player State")]
    public double density;
    public double velocity;
    public double gravityAcceleration;
    public double acceleration;
    public double currHeight;
    public double terminalVelocity;
    public bool active;

    [Header("Mass Transfer")]
    public AnimationCurve massCurve;
    [SerializeField] private bool debugMassCurve = false;

    [Header("References")]
    public PlayerScript otherPlayer;
    public Seesaw seesaw;
    private GameController gc;
    public GameObject backpack;
    public List<CharacterStats> characterStatStorage;

    [Header("Other Config")]
    public Side side;
    public float leftBoundary;
    public float rightBoundary;

    public Animator animator;
    private double apexHeight = 0;

    public enum Side
    {
        LEFT, RIGHT
    };

    public enum Character
    {
        BUG,
        VACUUM,
        HANDBAG,
        THIEF,
    }

    [System.Serializable]
    public class CharacterStats 
    {
        public Character character;
        public string Name;
        public AnimatorController controller;
    }

    public Character character;
    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        seesaw = FindObjectOfType<Seesaw>();
        animator = GetComponent<Animator>();
        if (side == Side.RIGHT)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (active)
        {
            gc.activePlayer = this;
            gc.InitializeGame();
        }
    }

    private double CalculateTerminalVelocity(double weight)
    {
        double innerValue = 2 * weight / (dragCoefficient * density * referenceArea);
        return math.pow(math.abs(innerValue), dragPower);
    }

    private void FixedUpdate()
    {
        if (!gc.gameStarted || !active)
        {
            return;
        }
        double scaledPlanetRadius = planetRadius * 1;
        gravityAcceleration = gravity * math.pow(scaledPlanetRadius / (scaledPlanetRadius + currHeight), 2);
        double weight = mass * -gravityAcceleration;
        double calculatedDensity = .0000233341d * 101325d * math.pow(1d - 0.0000225577 * currHeight, 5.25588d);
        density = useCustomDensity ? customAirDensity : calculatedDensity;
        double airRes = dragCoefficient * density * (math.pow(math.abs(velocity), 1 / dragPower) / 2) * referenceArea;
        terminalVelocity = CalculateTerminalVelocity(weight);
        if (velocity >= 0)
        {
            acceleration = (weight - airRes) / mass;
        }
        else
        {
            acceleration = (weight + airRes) / mass;
        }
        velocity += acceleration * Time.fixedDeltaTime;
        double prevHeight = currHeight;
        currHeight += velocity * Time.fixedDeltaTime;
        apexHeight = math.max(apexHeight, currHeight);
        transform.position = new Vector3(transform.position.x, (float)currHeight);
        if (currHeight < groundLevel)
        {
            CheckLeftRight();
        }
        else
        {
            FlyingObject[] hitObjects;
            if (prevHeight < currHeight)
            {
                hitObjects = gc.CheckCollision(prevHeight, currHeight, transform.position.x + leftBoundary, transform.position.x + rightBoundary);
                for (int i = 0; i < hitObjects.Length; ++i)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        animator.SetTrigger("CatchItem");
                    }
                    hitObjects[i].Hit(FlyingObject.HitDirection.UP, this);
                }
            }
            else
            {
                hitObjects = gc.CheckCollision(currHeight, prevHeight, transform.position.x + leftBoundary, transform.position.x + rightBoundary);
                for (int i = 0; i < hitObjects.Length; ++i)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        animator.SetTrigger("CatchItem");
                    }
                    hitObjects[i].Hit(FlyingObject.HitDirection.DOWN, this);
                }
            }
        }
        gc.UpdateCamera(currHeight);
    }

    public void IncreaseMass(double otherMass)
    {
        float transferred = massCurve.Evaluate((float) otherMass);
        mass += massCurve.Evaluate((float) otherMass);
        if (debugMassCurve) Debug.Log($"Picked up {otherMass}kg → Transferred {transferred}kg");
        UpdateBagMass();
    }

    public void DecreaseMass(double decrease)
    {
        mass -= decrease;
        UpdateBagMass();
    }

    public void UpdateBagMass()
    {
        backpack.transform.localScale = new Vector3((float)math.sqrt(mass) / 10f, (float)math.sqrt(mass) / 10f);
    }

    public void StartMovement(double startVelocity)
    {
        gc.UpdateCharacter(character);
        UpdateBagMass();
        if (startVelocity < 0)
        {
            gc.GameOver();
        }
        active = true;
        currHeight = 1f + groundLevel;
        gc.activePlayer = this;
        velocity = startVelocity;
    }

    public void HitSeesaw()
    {
        // double massDifference = mass - otherPlayer.mass;
        // double impulseVelocity = -velocity * .8;
        // int massSign = massDifference < 0 ? -1 : 1;
        // impulseVelocity += math.pow(math.abs(massDifference), 1d / 3d) * massSign;
        //double impulseVelocity = math.abs(velocity) * mass / otherPlayer.mass;
        // double impulseVelocity = math.abs(velocity) * math.pow(mass / otherPlayer.mass, 0.5);
        double energyLost = mass * gravity * apexHeight;
        double impulseVelocity = math.sqrt((2 * energyLost) / (otherPlayer.mass * 0.3));
        impulseVelocity += minimumImpulse; // Seesaw minimum impulse velocity
        //Debug.Log($"@@@@@ Impulse Velocity {impulseVelocity} - Peak {apexHeight}");
        otherPlayer.StartMovement(impulseVelocity);
        apexHeight = currHeight;
        active = false;
    }

    public void CheckLeftRight()
    {
        if (seesaw.left <= transform.position.x && transform.position.x <= seesaw.middle && side == Side.LEFT)
        {
            HitSeesaw();
        }
        else if (seesaw.middle <= transform.position.x && transform.position.x <= seesaw.right && side == Side.RIGHT)
        {
            HitSeesaw();
        }
        else
        {
            gc.GameOver();
        }
    }

    public void SetCharacter(Character name)
    {
        gc = FindObjectOfType<GameController>();
        Debug.Log("What's going on here");
        foreach (CharacterStats ch in characterStatStorage)
        {
            if (name == ch.character)
            {
                animator.runtimeAnimatorController = ch.controller;
                character = name;
                return;
            }
        }
        Debug.LogError("Attempting to set to a character that doesn't exist. Make sure that " + name.ToString() + " is inside the character stat storage in the playerScript.");
    }
}
