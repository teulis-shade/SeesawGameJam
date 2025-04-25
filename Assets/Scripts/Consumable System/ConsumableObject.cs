using System;
using UnityEngine;

public class ConsumableObject : FlyingObject
{
    public ConsumableData consumableData;

    void Awake()
    {
        height = transform.position.y;
    }

    protected override void OnCollect(PlayerScript player, GameController gc)
    {
        gc.TriggerConsumableCollected(consumableData);
    }

    protected override float GetInitialX()
    {
        return transform.position.x;
    }
}