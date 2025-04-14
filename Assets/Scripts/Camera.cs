using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] PlayerScript player1;
    [SerializeField] PlayerScript player2;
    //public Camera camera.main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.active) {
            //follow player 1
            //Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = player1.transform.position - new Vector3(0f, 0f, 10.0f);
        }
        else if (player2.active)
        {
            //follow player 2
            transform.position = player2.transform.position;
        }

        
    }
}
