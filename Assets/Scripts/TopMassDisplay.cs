using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TopMassDisplay : MonoBehaviour
{
    public TextMeshProUGUI leftMassText;
    public TextMeshProUGUI rightMassText;
    public BalanceBeamDisplay balanceBeamDisplay;
    public Color activeColor;
    public Color inactiveColor;

    public void SetPlayerMass(double mass, PlayerScript.Side side, bool isActive)
    {
        if (side == PlayerScript.Side.LEFT)
        {
            balanceBeamDisplay.SetLeftMass(mass);
            leftMassText.text = math.round(mass).ToString("#,0");
            leftMassText.color = isActive ? activeColor : inactiveColor;
        }
        else if (side == PlayerScript.Side.RIGHT)
        {
            balanceBeamDisplay.SetRightMass(mass);
            rightMassText.text = math.round(mass).ToString("#,0");
            rightMassText.color = isActive ? activeColor : inactiveColor;
        }
    }

    public void SetActivePlayer(PlayerScript.Side side)
    {
        if (side == PlayerScript.Side.LEFT)
        {
        }
        else if (side == PlayerScript.Side.RIGHT)
        {
            rightMassText.color = activeColor;
            leftMassText.color = inactiveColor;
        }
    }
}
