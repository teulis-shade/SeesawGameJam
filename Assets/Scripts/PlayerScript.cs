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
    public Side side;

    public enum Side
    {
        LEFT, RIGHT 
    };


    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        seesaw = FindObjectOfType<Seesaw>();
        if (active)
        {
            gc.activePlayer = this;
        }
        velocity = 0f;
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            return;
        }
        double gravity = 9.81 * math.pow((6371000 / (6371000 + currHeight)), 2);
        double density = .0000233341d * 101325d * math.pow(1d - .0000225577, 5.25588d);
        double airRes = .7296 * density * math.pow(velocity, 2);
        double acceleration;
        if (velocity >= 0)
        {
            acceleration = -(gravity * mass + airRes);
            acceleration /= mass;
        }
        else
        {
            acceleration = -(gravity * mass - airRes);
            acceleration /= mass;
        }
        velocity += acceleration / 50d;
        double prevHeight = currHeight;
        currHeight += velocity / 50d;
        transform.position = new Vector3(transform.position.x, (float)currHeight);
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

        gc.UpdateCamera(currHeight);
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
        currHeight = 1f;
        gc.activePlayer = this;
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
}
