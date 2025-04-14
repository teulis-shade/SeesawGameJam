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
    }

    private void IncreaseMass(double otherMass)
    {
        mass += otherMass;
    }

    public void StartMovement(double startVelocity)
    {
        active = true;
        velocity = startVelocity;
    }

    public void HitSeesaw()
    {
        otherPlayer.StartMovement(velocity);
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
