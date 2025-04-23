using UnityEngine;
using System.Collections;

public class RandomAudioPlayer : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] clips;
    public float minDelay = 1f;
    public float maxDelay = 4f;

    private AudioSource audioSource;
    private int lastPlayedIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomClips());
    }

    private IEnumerator PlayRandomClips()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            int newIndex;
            do
            {
                newIndex = Random.Range(0, clips.Length);
            } while (clips.Length > 1 && newIndex == lastPlayedIndex);

            lastPlayedIndex = newIndex;
            AudioClip clip = clips[newIndex];
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}