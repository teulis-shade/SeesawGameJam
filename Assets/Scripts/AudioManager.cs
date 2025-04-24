using UnityEngine;
using Unity.Mathematics;
using System.Runtime.CompilerServices;



public class AudioManager : MonoBehaviour
{
    private GameController gc;
    private WindFXController windFX;
    private MusicController musicController;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        windFX = GetComponentInChildren<WindFXController>();
        musicController = GetComponentInChildren<MusicController>();
        gc.OnCharacterChanged += musicController.ChangeMusic;
    }

    void Update()
    {
        windFX.SetVelocity(math.abs(gc.activePlayer.velocity));
    }

    void OnDisable()
    {
        gc.OnCharacterChanged -= musicController.ChangeMusic;
    }


}
