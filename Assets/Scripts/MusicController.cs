using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [System.Serializable]
    public class CharacterMusic
    {
        public PlayerScript.Character character;
        public AudioClip music;
    }

    List<CharacterMusic> musicStorage;

    private AudioSource source;
    private AudioSource characterSource;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        characterSource = gameObject.AddComponent<AudioSource>();
        characterSource.loop = true;
        characterSource.Pause();
    }

    public void ChangeMusic(PlayerScript.Character character)
    {
        if (!characterSource.isPlaying)
        {
            source.Play();
            source.time = 0;
            characterSource.time = 0;
        }
        foreach (CharacterMusic music in musicStorage)
        {
            if (music.character == character)
            {
                float characterTime = characterSource.time;
                characterSource.clip = music.music;
                characterSource.Play();
                characterSource.time = characterTime;
                return;
            }
        }
        Debug.LogError("Attempting to change to a character that doesn't exist.");
    }
}
