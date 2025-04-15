using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    [SerializeField] PlayerScript player1;
    [SerializeField] PlayerScript player2;
    [SerializeField] Image img;
    private GameController gc;
    //public Camera camera.main;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.active) {
            //follow player 1
            //Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = player1.transform.position - new Vector3(0f, 0f, 10.0f);
            RectTransform rt = img.rectTransform;
            //rt.anchoredPosition += new Vector2(0, 10);
            rt.anchoredPosition = new Vector2(0f, (float) (gc.activePlayer.currHeight / 300000000f)*-3800f+1900f);
            
            
        }
        else if (player2.active)
        {
            //follow player 2
            transform.position = player2.transform.position - new Vector3(0f, 0f, 10.0f);
        }

        
    }
}
