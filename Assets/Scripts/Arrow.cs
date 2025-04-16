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

        //seesaw
        RectTransform rt = img.rectTransform;

        Vector2 arrow = seesaw.transform.position - gc.activePlayer.transform.position;
        //arrow = arrow.normalized*50;
        arrow = arrow.normalized;
        arrow = new Vector2 (arrow.x*50f, -200);
        rt.anchoredPosition = arrow;

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
