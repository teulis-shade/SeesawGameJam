using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallCameraController : MonoBehaviour
{
    public GameObject follow;

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position - new Vector3(0f, 0f, 10f);

    }
}
