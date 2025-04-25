using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class BalanceBeamDisplay : MonoBehaviour
{

    [SerializeField]
    private float leftRotationBound = -22f;
    [SerializeField]
    private float rightRotationBound = 22f;
    
    public float targetAngle;
    public float rotationSpeed = 5f;
    public double leftMass = 10d;
    public double rightMass = 10d;

    public void SetLeftMass(double mass)
    {
        leftMass = mass;
    }

    public void SetRightMass(double mass)
    {
        rightMass = mass;
    }

    void Update()
    {
        double totalMass = math.abs(leftMass) + math.abs(rightMass);

        float mappedAngle = 0;
        if (totalMass != 0)
        {
            double normLeft = leftMass / totalMass;
            double normRight = rightMass / totalMass;
            double massDifference = normLeft - normRight;
            float md = (float) massDifference;
            float curvedDifference = Mathf.Sign(md) * Mathf.Pow(Mathf.Abs(md), 0.2f);
            mappedAngle = Mathf.Lerp(leftRotationBound, rightRotationBound, (curvedDifference + 1f) / 2f);
        }

        float clampedTargetAngle = Mathf.Clamp(mappedAngle, leftRotationBound, rightRotationBound);
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedTargetAngle);
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = newRotation;
        foreach (Transform child in transform)
        {
            child.localRotation = Quaternion.Inverse(transform.localRotation);
        }
    }
}
