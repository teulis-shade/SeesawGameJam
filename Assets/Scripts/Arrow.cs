using System;
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
        float side;
        //if (gc.activePlayer.side == 0) //LEFT
        if (gc.activePlayer.side == PlayerScript.Side.LEFT) //LEFT
        {
            side = seesaw.left + seesaw.middle;
            side /= 2;
        }
        else
        {
            side = seesaw.right + seesaw.middle;
            side /= 2;
        }


        //seesaw
        RectTransform rt = img.rectTransform;

        //Vector2 arrow = seesaw.transform.position.x + side - gc.activePlayer.transform.position.x;
        float arrow = cam.transform.position.x - side - 25f;
        arrow = -arrow / 50f;
        if (arrow > 1f)
        {
            arrow = 1f;
        }
        else if (arrow < 0f)
        {
            arrow = 0f;
        }
        arrow = arrow * 750 - 375;
        rt.anchoredPosition = new Vector2(arrow, -200);

        //Rotates Arrow
            //ToDO: make it rotate towards players seat
        //Vector2 arrow_angle = seesaw.transform.position - gc.activePlayer.transform.position - new Vector3(side -25f, 0f);
        Vector2 arrow_angle = seesaw.transform.position - cam.transform.position;
        Vector2 m_MyFirstVector = Vector2.down;
        float m_Angle = Vector2.Angle(m_MyFirstVector, arrow_angle);
        float sign = Mathf.Sign(Vector3.Cross(m_MyFirstVector, arrow_angle).z);
        m_Angle *= sign;
        rt.rotation = Quaternion.Euler(0, 0, m_Angle);





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
