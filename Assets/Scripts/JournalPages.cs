using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalPages : MonoBehaviour
{
    public List<JournalCheck.JournalEntry> journalEntries;

    private int index;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image sprite;

    private void OnEnable()
    {
        journalEntries = FindObjectOfType<JournalCheck>().CheckEntries();
        index = 0;
        UpdateInfo(index);
    }

    void UpdateInfo(int index)
    {
        if (journalEntries.Count == 0)
        {
            nameText.text = "Nothing(yet)";
            descriptionText.text = "Get out there and get some stuff";
            sprite.sprite = null;
            return;
        }
        nameText.text = journalEntries[index].name;
        descriptionText.text = journalEntries[index].description;
        sprite.sprite = journalEntries[index].sprite;
    }

    public void MoveRight()
    {
        UpdateInfo(Modulus(++index, journalEntries.Count));
    }

    public void MoveLeft()
    {
        UpdateInfo(Modulus(--index, journalEntries.Count));
    }

    public int Modulus(int number, int mod)
    {
        if (mod == 0)
        {
            return 0;
        }
        int returnNumber = number % mod;
        return returnNumber < 0 ? returnNumber + mod : returnNumber;
    }
}
