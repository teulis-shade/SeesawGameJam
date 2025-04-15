using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawAudioRandomizer : MonoBehaviour
{
    public AudioClip[] leftSideSounds;
    public AudioClip[] rightSideSounds;
    [Header("Starting Side (True = Left, False = Right)")]
    public bool sideBoolean;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Temporary testing code
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         PlaySound();
    //     }
    // }

    // Call to play seesaw sound (swaps side after playing)
    void PlaySound()
    {
        PlaySound(sideBoolean);
        sideBoolean = !sideBoolean;
    }

    // Call to play seesaw sound from a specific side (left/right)
    void PlaySound(bool sideBoolean)
    {
        AudioClip[] sounds = sideBoolean ? leftSideSounds : rightSideSounds;
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.PlayOneShot(source.clip);
    }

}
