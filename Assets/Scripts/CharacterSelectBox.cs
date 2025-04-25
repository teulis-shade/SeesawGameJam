using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectBox : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public PlayerScript.Character character;
    public CharacterSelect selection;
    public void OnPointerEnter(PointerEventData data)
    {
        selection.UpdateCharacter(((RectTransform)transform).anchorMin.x, ((RectTransform)transform).anchorMax.x);
    }

    public void OnPointerClick(PointerEventData data)
    {
        selection.SelectCharacter(((RectTransform)transform).anchorMin.x, ((RectTransform)transform).anchorMax.x, character);
    }
}
