using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
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


    public void UpdateCamera(double height)
    {
        transform.position = new Vector3(Mathf.Clamp(gc.activePlayer.transform.position.x, -10f, 10f), gc.activePlayer.transform.position.y, -10f);
        //follow player 1
        //Camera.main.ScreenToWorldPoint(mousePosition);
        RectTransform rt = img.rectTransform;
        //rt.anchoredPosition += new Vector2(0, 10);
        rt.localPosition = new Vector2((float) (-transform.position.x * 47), (float) (height / 3000f)*-1700f+850f);//can update 3000 (if needed)
    }
}
