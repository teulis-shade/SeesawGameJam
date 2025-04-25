using UnityEngine;

public abstract class ConsumableData : ScriptableObject
{
    public KeyCode activationKey;
    public AudioClip audioClip;
    public abstract void ApplyEffect(PlayerScript player);
}

