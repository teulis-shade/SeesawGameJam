using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    public int height;

    public void SetHeight(int height)
    {
        textMeshPro.text = $"{height:N0} m";
    }

    void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
}
