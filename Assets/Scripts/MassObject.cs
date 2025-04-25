using UnityEngine;

public abstract class MassObject : FlyingObject
{
    public double mass;

    protected override void OnCollect(PlayerScript player, GameController gc)
    {
        player.IncreaseMass(mass);
    }


    protected override float GetInitialX()
    {
        //This will depend on the object, for now, returns a random number between -30 and 30
        return Random.Range(-18f, 18f);
    }
}