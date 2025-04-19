using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearestArrow : MonoBehaviour
{
    //turn back to private
    public FlyingObject flyingObject;
    private Image img;
    private GameController gc;
    private Camera cam;
    //[SerializeField] GameObject go;
    // Start is called before the first frame update
    //private Seesaw seesaw;

    public FlyingObject GetFlyingObject() { return flyingObject; }
    public void SetFlyingObject(FlyingObject fo) { flyingObject = fo; }

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        cam = FindObjectOfType<Camera>();
        img = GetComponent<Image>();
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO:

        //if has no object, should dissapear from screen or turn invisible
        if (flyingObject == null)
        {
            //TODO: 
            //disapear from screen
        }
        else
        {
            //TODO:
            //do tracking and moving

            //try 2
            RectTransform rt = img.rectTransform;
            Vector2 foVector = new Vector2(flyingObject.transform.position.x, flyingObject.transform.position.y);
            //Vector2 cameraVector = new Vector2(cam.transform.position.x, cam.transform.position.y);
            Vector2 cameraVector = new Vector2(gc.activePlayer.transform.position.x, gc.activePlayer.transform.position.y);

            //Move
            Vector2 camToFlyingObject = foVector - cameraVector;
            camToFlyingObject = camToFlyingObject.normalized;
            camToFlyingObject = camToFlyingObject * 50f;
            rt.anchoredPosition = camToFlyingObject;

            //Rotate
            
            
            float m_Angle = Vector2.Angle(foVector, cameraVector);
            float sign = Mathf.Sign(Vector3.Cross(foVector, cameraVector).z);
            m_Angle *= sign;
            rt.rotation = Quaternion.Euler(0, 0, m_Angle);


            Debug.DrawLine(foVector, cameraVector, Color.red);

            /*
            //Vector2 arrow_angle = flyingObject.transform.position - cam.transform.position;
            //Vector2 m_MyFirstVector = Vector2.down;
            //float m_Angle = Vector2.Angle(new Vector2(flyingObject.transform.position.x, flyingObject.transform.position.y), new Vector2(cam.transform.position.x, cam.transform.position.y));
            float m_Angle = Vector2.Angle(new Vector2(flyingObject.transform.position.x, flyingObject.transform.position.y), new Vector2(cam.transform.position.x, cam.transform.position.y));
            float sign = Mathf.Sign(Vector3.Cross(m_MyFirstVector, arrow_angle).z);
            m_Angle *= sign;
            rt.rotation = Quaternion.Euler(0, 0, m_Angle);
            */

            //try 1
            /*
            RectTransform rt = img.rectTransform;
            Vector2 arrow_angle = flyingObject.transform.position - cam.transform.position;
            Vector2 m_MyFirstVector = Vector2.down;
            float m_Angle = Vector2.Angle(m_MyFirstVector, arrow_angle);
            float sign = Mathf.Sign(Vector3.Cross(m_MyFirstVector, arrow_angle).z);
            m_Angle *= sign;
            rt.rotation = Quaternion.Euler(0, 0, m_Angle);
            */
        }


        //Rotates Arrow
        //ToDO: make it rotate towards players seat
        //Vector2 arrow_angle = seesaw.transform.position - gc.activePlayer.transform.position - new Vector3(side -25f, 0f);
        //Vector2 arrow_angle = seesaw.transform.position - cam.transform.position;






    }
}
