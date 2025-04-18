using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        return left + transform.position.x <= xPos && xPos <= right + transform.position.x;
    }

    public virtual void Hit(HitDirection dir, PlayerScript player)
    {
        player.IncreaseMass(mass);
        FindObjectOfType<GameController>().RemoveFlyer(this);
        Destroy(this.gameObject);
    }

    public void Initialize()
    {
        transform.position = new Vector3(GetInitialX(), (float)height);
    }

    protected virtual float GetInitialX()
    {
        //This will depend on the object, for now, returns a random number between -30 and 30
        return Random.Range(-30f, 30f);
    }
}
