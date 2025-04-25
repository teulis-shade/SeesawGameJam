using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Boost Consumable")]
public class BoostConsumable : ConsumableData
{
    public float boostVelocity;
    public override void ApplyEffect(PlayerScript player)
    {
        player.velocity = boostVelocity;
    }
}