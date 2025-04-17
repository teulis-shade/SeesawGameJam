using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    private GameController gc;
    [SerializeField] HeightDisplay heightDisplay;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        heightDisplay.SetHeight((int) gc.activePlayer.currHeight);
    }
}
