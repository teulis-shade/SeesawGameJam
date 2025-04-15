using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBar : MonoBehaviour
{
    [SerializeField] Image img;
    //[SerializeField] GameObject go;
    // Start is called before the first frame update
    private GameController gc;


    void Start()
    {
        gc = FindObjectOfType<GameController>();

    }
    // Update is called once per frame
    void Update()
    {
        //if I want to make a bar
        //RectTransform rt = img.rectTransform;
        //rt.anchoredPosition = new Vector2(100, 200);
    }
}
