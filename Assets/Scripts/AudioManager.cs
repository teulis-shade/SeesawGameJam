using UnityEngine;
using Unity.Mathematics;



public class AudioManager : MonoBehaviour
{
    private GameController gc;
    private WindFXController windFX;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        windFX = GetComponent<WindFXController>();
    }

    void Update()
    {
        windFX.SetVelocity(math.abs(gc.activePlayer.velocity));
    }


}
