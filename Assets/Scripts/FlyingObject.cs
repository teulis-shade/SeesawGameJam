using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public double mass;
    public double height;

    public double left;
    public double right;

    public enum HitDirection
    {
        UP,
        DOWN,
    };

    public bool CheckCollision(double xPos)
    {
        return left <= xPos && xPos <= right;
    }

    public virtual void Hit(HitDirection dir, PlayerScript player)
    {
        player.IncreaseMass(mass);
    }
}
