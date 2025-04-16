using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] Image img;
    private GameController gc;
    private Camera cam;
    //[SerializeField] GameObject go;
    // Start is called before the first frame update
    private Seesaw seesaw;



    void Start()
    {
        seesaw = FindObjectOfType<Seesaw>();
        gc = FindObjectOfType<GameController>();
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the arrow towards seesaw

        float side = 15;
        //if (gc.activePlayer.side == 0) //LEFT
        if (gc.activePlayer.side == PlayerScript.Side.LEFT) //LEFT
        {
            side = -side;
        }

        //seesaw
        RectTransform rt = img.rectTransform;

        //Vector2 arrow = seesaw.transform.position.x + side - gc.activePlayer.transform.position.x;
        float arrow = side - gc.activePlayer.transform.position.x;
        rt.anchoredPosition = new Vector2(arrow, -200);




        //OG
        //Vector2 arrow = seesaw.transform.position.x + side - gc.activePlayer.transform.position.x;
        //arrow = new Vector2(arrow.x, 0);
        //arrow = new Vector2(arrow.x, -200);
        //rt.anchoredPosition = arrow;


        /*
        rt.anchoredPosition = new Vector2(100, 200);
        //rt.anchoredPosition = new Vector2(100, 200);
        cam.ScreenToWorldPoint()

        img.
        gc.activePlayer.transform.position.x = seesaw.
        //seesaw.transform.position = Vector3.zero;
        */
    }
}
