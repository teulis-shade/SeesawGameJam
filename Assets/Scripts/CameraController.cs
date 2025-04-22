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
    [Range(5, 50)]
    [SerializeField] private int zoomStartHeight = 10;
    [Range(5, 25)]
    [SerializeField] private int zoomedOutSize = 20;
    [Range(5, 25)]
    [SerializeField] private int defaultSize = 10;
    [Range(1f, 8f)]
    [SerializeField] private float zoomLerpSpeed = 5f; 

    public double height;
    private GameController gc;
    private Camera cam;

    //public Camera camera.main;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        cam = GetComponent<Camera>();
    }


    public void UpdateCamera(double height)
    {
        this.height = height;
        Vector3 calcTransform = new Vector3(Mathf.Clamp(gc.activePlayer.transform.position.x, -10f, 10f), gc.activePlayer.transform.position.y, -10f);
        float targetX = calcTransform.x;
        float targetSize = defaultSize;
        if (height < zoomStartHeight)
        {
            float t = Mathf.InverseLerp(0f, zoomStartHeight, (float) height);
            targetX = Mathf.Lerp(0f, calcTransform.x, t);
            targetSize = Mathf.Lerp(zoomedOutSize, defaultSize, t);
        }
        calcTransform.x = targetX;
        transform.position = calcTransform;
        if (img != null)
        {
            RectTransform rt = img.rectTransform;
            rt.localPosition = new Vector2((float) (-transform.position.x * 47), (float) (height / 3000f) * -1700f + 1788f); //can update 3000 (if needed)
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomLerpSpeed);
    }
}
