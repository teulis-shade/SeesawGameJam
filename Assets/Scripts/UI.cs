using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    private GameController gc;
    [SerializeField] HeightDisplay heightDisplay;
    [SerializeField] TopMassDisplay topMassDisplay;
    [SerializeField] VelocityText velocityText;

    void SetMassDisplay(PlayerScript player, bool isActive)
    {
        topMassDisplay.SetPlayerMass(player.mass, player.side, isActive);
    }
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        heightDisplay.SetHeight((int) gc.activePlayer.currHeight);
        SetMassDisplay(gc.activePlayer, true);
        SetMassDisplay(gc.activePlayer.otherPlayer, false);
        velocityText.SetPlayerVelocity(gc.activePlayer.velocity);
    }
}
