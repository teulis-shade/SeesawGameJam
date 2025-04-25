using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TopMassDisplay : MonoBehaviour
{
    [Header("Color Config")]
    public Color activeColor;
    public Color inactiveColor;
    public Color dangerColor;

    [Header("Image References")]
    public Image[] balanceBeamImages;

    [Header("Other References")]
    public TextMeshProUGUI leftMassText;
    public TextMeshProUGUI rightMassText;
    public BalanceBeamDisplay balanceBeamDisplay;

    public double leftMass = 10d;
    public double rightMass = 10d;
    private PlayerScript.Side activeSide = PlayerScript.Side.LEFT;

    void Update()
    {
        foreach (var image in balanceBeamImages)
        {
            image.color = GetMassInfluencedColor(activeSide, true);
        }
    }

    private Color GetMassInfluencedColor(PlayerScript.Side side, bool isActive)
    {
        if (!isActive) return inactiveColor;
        if (side == PlayerScript.Side.LEFT)
        {
            return rightMass > leftMass ? dangerColor : activeColor;
        }
        else
        {
            return leftMass > rightMass ? dangerColor : activeColor;
        }
    }

    public void SetPlayerMass(double mass, PlayerScript.Side side, bool isActive)
    {
        if (side == PlayerScript.Side.LEFT)
        {
            leftMass = mass;
            balanceBeamDisplay.SetLeftMass(mass);
            leftMassText.text = math.round(mass).ToString("#,0");
            leftMassText.color = GetMassInfluencedColor(side, isActive);
        }
        else if (side == PlayerScript.Side.RIGHT)
        {
            rightMass = mass;
            balanceBeamDisplay.SetRightMass(mass);
            rightMassText.text = math.round(mass).ToString("#,0");
            rightMassText.color = GetMassInfluencedColor(side, isActive);
        }
        if (isActive) activeSide = side;
    }
}
