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
        windFX = GetComponent<WindFXController>();
        musicController = GetComponent<MusicController>();
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
