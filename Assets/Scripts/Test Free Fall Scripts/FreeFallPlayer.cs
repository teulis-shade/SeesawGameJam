using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FreeFallPlayer : MonoBehaviour
{

    [Header("Configurable Values")]
    [Range(40f, 150f)]
    public float mass = 70f; // kg
    [Range(0.1f, 40f)]
    public float gravity = 9.81f; // m / s^2
    [Range(0.01f, 5f)]
    public float referenceArea = 0.140f; // m^2
    [Range(0.01f, 1000f)]
    public float airDensity = 1.21f; // kg / m^3
    [Range(0f, 2f)]
    public float dragCoefficient = 0.7f;
    [Range(2000, 70000)]
    public int planetRadius = 6371; // km
    [Range(-100, 100)]
    public int groundLevel = -80; // m (not scaled)

    [Header("Motion State")]
    public double velocity;
    public double acceleration;
    public double currentHeight;
    public double weight;
    public double terminalVelocity;

    [Header("References")]
    public TMP_Text massText;

    private float scaleValue = 1.0f;
    private float jumpForce = 30f;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxCollider;
    private bool isTouchingGround = false;

    public void StopMovementOnContact(float contactPoint)
    {
        velocity = 0;
        acceleration = 0;
        double halfHeight =  playerBoxCollider.size.y * transform.localScale.y * 0.5;
        float snapHeight = contactPoint + (float) halfHeight;
        playerRigidbody.MovePosition(new Vector3(transform.position.x, snapHeight));
        currentHeight = snapHeight;
        isTouchingGround = true;
    }

    public void ApplyInstantVelocity(double instantVelocity)
    {
        velocity += instantVelocity;
    }

    double CalculateDrag(double velocity)
    {
        double scaledDensity = airDensity * scaleValue;
        double scaledArea = referenceArea * scaleValue;
        double scaledDragCoefficient = dragCoefficient * scaleValue;
        return scaledDragCoefficient * scaledDensity * (math.pow(velocity, 2) / 2) * scaledArea;
    }

    double CalculateWeight()
    {
        double scaledMass = mass * scaleValue;
        double scaledGravity = gravity * scaleValue;
        double scaledPlanetRadius = planetRadius * scaleValue * 1000; // km to m
        double gravityAcceleration = scaledGravity * math.pow(scaledPlanetRadius / (scaledPlanetRadius + currentHeight - groundLevel), 2);
        return scaledMass * -gravityAcceleration;
    }

    double CalculateAcceleration(double weight)
    {
        double scaledMass = mass * scaleValue;
        double drag = CalculateDrag(velocity);
        if (velocity > 0) {
            return (weight - drag) / scaledMass;
        } else {
            return (weight + drag) / scaledMass;
        }
    }

    double CalculateTerminalVelocity(double weight)
    {
        double scaledDensity = airDensity * scaleValue;
        double scaledArea = referenceArea * scaleValue;
        double scaledDragCoefficient = dragCoefficient * scaleValue;
        double innerValue = 2 * weight / (scaledDragCoefficient * scaledDensity * scaledArea);
        return math.pow(math.abs(innerValue), 0.5);
    }

    void Start()
    {
        velocity = 0f;
        mass = 70f;
        gravity = 9.81f;
        referenceArea = 0.140f;
        airDensity = 1.21f;
        dragCoefficient = 0.7f;
        planetRadius = 6371;

        // velocity = 0f;
        // mass = 100f;
        // gravity = 30f;
        // referenceArea = 1.26f;
        // airDensity = 1.8f;
        // dragCoefficient = 0.9f;
        // planetRadius = 6371;

        //scaleValue = 1.0f;
        currentHeight = transform.position.y;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        weight = CalculateWeight();
        double drag = CalculateDrag(velocity);
        if (isTouchingGround && velocity <= 0f)
        {
            acceleration = 0;
            velocity = 0;
        }
        else
        {
            acceleration = CalculateAcceleration(weight);
        }
        velocity += acceleration * Time.fixedDeltaTime;
        currentHeight += velocity * Time.fixedDeltaTime;
        terminalVelocity = CalculateTerminalVelocity(weight);
        // transform.position = new Vector3(transform.position.x, (float) currentHeight);
        playerRigidbody.MovePosition(new Vector3(transform.position.x, (float) currentHeight));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Collide if not seesaw
        if (collision.gameObject.GetComponent<FreeFallSeesaw>() == null) {
            //velocity = 0;
            //acceleration = 0;
            //double halfHeight =  playerBoxCollider.size.y * transform.localScale.y * 0.5;
            ////Debug.LogFormat("Half Height: {0}, Contact Point: {1}", halfHeight, collision.contacts[0].point.y);
            //playerRigidbody.MovePosition(new Vector3(transform.position.x, collision.contacts[0].point.y + (float) halfHeight));
            ////Vector3 tempVector = new Vector3(transform.position.x, collision.contacts[0].point.y + (float) halfHeight);
            ////Debug.Log(tempVector);
            ////Debug.Log("Rigidbody Position: " + transform.position);
            //currentHeight = collision.contacts[0].point.y + (float) halfHeight;
            //isTouchingGround = true;
            StopMovementOnContact(collision.contacts[0].point.y);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("COLLISION EXIT");
        isTouchingGround = false;
    }

    void Update()
    {
        massText.text = mass.ToString("F1") + " kg";
        if (Input.GetKeyDown(KeyCode.S) && isTouchingGround)
        {
            ApplyInstantVelocity(jumpForce);
        }
    }
}
