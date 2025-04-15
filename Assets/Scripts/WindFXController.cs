using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFXController : MonoBehaviour
{
    [Range(0f, 2f)]
    public float windStrength = 0f;
    [Range(0f, 1f)]
    public float globalVolume = 1f;
    public List<AudioSource> windSources = new List<AudioSource>();

    void Update()
    {
        int lowerIndex = Mathf.FloorToInt(windStrength);
        int upperIndex = Mathf.Min(lowerIndex + 1, windSources.Count - 1);
        float t = windStrength - lowerIndex;
        for (int i = 0; i < windSources.Count; i++)
        {
            if (i == lowerIndex)
                windSources[i].volume = (1f - t) * globalVolume;
            else if (i == upperIndex)
                windSources[i].volume = t * globalVolume;
            else
                windSources[i].volume = 0f * globalVolume;
        }
    }

    void Start()
    {
        foreach (var source in windSources) {
            if (!source.isPlaying)
                source.Play();
        }
    }

}
