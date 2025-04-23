using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResizableParallaxLayer
{
    public SpriteRenderer spriteRenderer;
    public Vector2 parallaxMultiplier = new Vector2(0.1f, 0.6f);
    
}

public class ResizableParallaxController : MonoBehaviour
{
    public Vector2 goalSize = new Vector2(50, 6000);
    public Vector2 cameraUnitSize = new Vector2(36f, 30f);
    public Vector2 movementMultiplier = new Vector2(0.3f, 0.977693f);
    public ResizableParallaxLayer[] layers;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = layers[0].spriteRenderer.sprite;
        Vector2 resolution = new Vector2(sprite.rect.width / sprite.pixelsPerUnit, sprite.rect.height / sprite.pixelsPerUnit);
        Vector2 targetSize = resolution - cameraUnitSize;
        Vector2 numerator = goalSize - targetSize;
        movementMultiplier = new Vector2(numerator.x / goalSize.x, numerator.y / goalSize.y);
    }

    void LateUpdate()   
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        foreach (ResizableParallaxLayer child in layers)
        {
            Transform childTransform = child.spriteRenderer.transform;
            childTransform.position += new Vector3(deltaMovement.x * child.parallaxMultiplier.x * movementMultiplier.x, deltaMovement.y * child.parallaxMultiplier.y * movementMultiplier.y, 0);

        }
        lastCameraPosition = cameraTransform.position;
    }
}
