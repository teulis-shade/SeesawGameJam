using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layer;
    public Vector2 parallaxMultiplier = new Vector2(0.1f, 0.6f);
    
}

public class ParallaxController : MonoBehaviour
{
    public ParallaxLayer[] layers;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        foreach (ParallaxLayer child in layers)
        {
            child.layer.position += new Vector3(deltaMovement.x * child.parallaxMultiplier.x, deltaMovement.y * child.parallaxMultiplier.y, 0);

        }
        lastCameraPosition = cameraTransform.position;
    }
}
