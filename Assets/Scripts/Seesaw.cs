using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public float left;
    public float right;

    private void Start()
    {
        left = -.25f;
        right = .25f;
    }
}
