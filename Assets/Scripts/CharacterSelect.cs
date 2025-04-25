using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    PlayerScript.Side currentSide;
    public GameObject leftArrow;
    public GameObject rightArrow;

    private void Start()
    {
        currentSide = PlayerScript.Side.LEFT;
    }

    private void Update()
    {
        if (currentSide == PlayerScript.Side.LEFT && rightArrow.activeSelf)
        {
            rightArrow.SetActive(false);
        }
        if (currentSide == PlayerScript.Side.RIGHT && !rightArrow.activeSelf)
        {
            rightArrow.SetActive(true);
        }
    }

    public void UpdateCharacter(float min, float max)
    {
        float mid = (max + min) / 2;
        if (currentSide == PlayerScript.Side.LEFT)
        {
            ((RectTransform)leftArrow.transform).anchorMin = new Vector2(min, .075f);
            ((RectTransform)leftArrow.transform).anchorMax = new Vector2(mid, .175f);
        }
        else
        {
            ((RectTransform)rightArrow.transform).anchorMin = new Vector2(mid, .075f);
            ((RectTransform)rightArrow.transform).anchorMax = new Vector2(max, .175f);
        }
    }

    public void SelectCharacter(float min, float max, PlayerScript.Character character)
    {
        var player = FindObjectOfType<PlayerScript>();
        if (player.side != currentSide)
        {
            player = player.otherPlayer;
        }

        float mid = (max + min) / 2;
        if (currentSide == PlayerScript.Side.LEFT)
        {
            ((RectTransform)leftArrow.transform).anchorMin = new Vector2(min, .075f);
            ((RectTransform)leftArrow.transform).anchorMax = new Vector2(mid, .175f);
        }
        else
        {
            ((RectTransform)rightArrow.transform).anchorMin = new Vector2(mid, .075f);
            ((RectTransform)rightArrow.transform).anchorMax = new Vector2(max, .175f);
        }

        player.SetCharacter(character);
        
        if (currentSide == PlayerScript.Side.RIGHT)
        {
            FindObjectOfType<GameController>().StartGame();
            gameObject.SetActive(false);
        }
        currentSide++;
    }
}
