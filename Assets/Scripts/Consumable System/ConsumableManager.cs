using UnityEngine;
using System.Collections.Generic;
using System;


public class ConsumableManager : MonoBehaviour
{
    public List<ConsumableData> consumables;
    private Dictionary<Type, int> counts;
    private GameController gc;
    private AudioSource audioSource;
    public AudioClip onPickup;

    void Start()
    {
        // Get GameController instance
        counts = new Dictionary<Type, int>();
        gc = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
        // Set all consumable counts to zero
        foreach (var c in consumables)
        {
            counts[c.GetType()] = 0;
        }
        // Subscribe to consumable collected event
        gc.OnConsumableCollected += AddConsumable;
    }

    void Update()
    {
        foreach (var c in consumables)
        {
            if (Input.GetKeyDown(c.activationKey))
            {
                UseConsumable(c);
            }
        }
    }

    void OnDisable()
    {
        // Subscribe to consumable collected event
        gc.OnConsumableCollected -= AddConsumable;
    }

    public void AddConsumable(ConsumableData consumable)
    {
        if (consumable.audioClip != null)
        {
            audioSource.PlayOneShot(onPickup);
        }
        counts[consumable.GetType()]++;
    }

    public bool UseConsumable(ConsumableData consumable)
    {
        if (counts[consumable.GetType()] > 0)
        {
            counts[consumable.GetType()]--;
            if (consumable.audioClip != null)
            {
                audioSource.PlayOneShot(consumable.audioClip);
            }
            consumable.ApplyEffect(gc.activePlayer);
            return true;
        }
        return false;
    }

    public int GetConsumableCount(ConsumableData consumable)
    {
        return counts[consumable.GetType()];
    }
}