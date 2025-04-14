using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerScript : MonoBehaviour
{
    public double velocity;
    public double currHeight;
    public double mass;
    public bool active;
    public PlayerScript otherPlayer;
    public Seesaw seesaw;
    private GameController gc;


    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        double gravity = 9.81 * math.pow((6371000 / 6371000 + currHeight), 2);
        double density = .0000233341 * (101325 * math.pow(1 - .0000225577 * currHeight, 5.25588));
        double airRes = .7296 * density * math.pow(velocity, 2);
        double acceleration;
        if (velocity > 0)
        {
            acceleration = -(gravity * mass + airRes);
        }
        else
        {
            acceleration = -(gravity * mass - density);
        }
        velocity += acceleration / 50d;
        double prevHeight = currHeight;
        currHeight += velocity / 50d;
        if (currHeight < 0)
        {
            CheckLeftRight();
        }
        else
        {
            FlyingObject[] hitObjects;
            if (prevHeight < currHeight)
            {
                hitObjects = gc.CheckCollision(prevHeight, currHeight, transform.position.x);
                foreach (FlyingObject obj in hitObjects)
                {
                    obj.Hit(FlyingObject.HitDirection.UP, this);
                }
            }
            else
            {
                hitObjects = gc.CheckCollision(currHeight, prevHeight, transform.position.x);
                foreach (FlyingObject obj in hitObjects)
                {
                    obj.Hit(FlyingObject.HitDirection.DOWN, this);
                }
            }
        }
    }

    public void IncreaseMass(double otherMass)
    {
        mass += otherMass;
    }

    public void DecreaseMass(double decrease)
    {
        mass -= decrease;
    }

    public void StartMovement(double startVelocity)
    {
        active = true;
        velocity = startVelocity;
    }

    public void HitSeesaw()
    {
        double massDifference = mass - otherPlayer.mass;
        double impulseVelocity = -velocity;
        impulseVelocity -= math.pow(massDifference, 3) / 100f;
        otherPlayer.StartMovement(impulseVelocity);
        active = false;
    }

    public void CheckLeftRight()
    {
        if (seesaw.left <= transform.position.x && transform.position.x <= seesaw.right)
        {
            HitSeesaw();
        }
        else
        {
            gc.GameOver();
        }
    }
}
