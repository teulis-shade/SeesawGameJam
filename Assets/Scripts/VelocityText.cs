using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VelocityText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    public void SetPlayerVelocity(double velocity)
    {
        textMeshProUGUI.text = velocity.ToString("#,0") + " m/s";
    }
}
