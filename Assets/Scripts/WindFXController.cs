using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFXController : MonoBehaviour
{

    public AudioClip windSound;

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && windSound != null) {
            audioSource.clip = windSound;
            audioSource.Play();
        }
    }

}
