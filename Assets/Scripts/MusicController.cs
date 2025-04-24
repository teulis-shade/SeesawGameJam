using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource source;

    [System.Serializable]
    public class CharacterMusic
    {
        public PlayerScript.Character character;
        public AudioClip music;
    }

    public List<CharacterMusic> musicStorage;

    public void ChangeMusic(PlayerScript.Character character)
    {
        foreach (CharacterMusic music in musicStorage)
        {
            if (music.character == character)
            {
                float characterTime = source.time;
                source.clip = music.music;
                source.Play();
                source.time = characterTime;
                return;
            }
        }
        Debug.LogError("Attempting to change to a character that doesn't exist.");
    }
}
