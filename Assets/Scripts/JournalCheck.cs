using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalCheck : MonoBehaviour
{
    [System.Serializable]
    public class JournalEntry
    {
        public string name;
        public string description;
        public Sprite sprite;

        public JournalEntry(string name, string description, Sprite sprite)
        {
            this.name = name;
            this.description = description;
            this.sprite = sprite;
        }
    }

    public List<JournalEntry> journalEntries = new List<JournalEntry>();

    public List<JournalEntry> CheckEntries()
    {
        List<JournalEntry> collectedEntries = new List<JournalEntry>();
        foreach (var entry in journalEntries)
        {
            string key = entry.name + "Gotten";
            if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1)
            {
                collectedEntries.Add(entry);
            }
        }
        return collectedEntries;
    }

    public void GetObject(string objectName)
    {
        string key = objectName + "Gotten";
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) == 0)
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }

    public void ClearEntries()
    {
        foreach (var entry in journalEntries)
        {
            if (PlayerPrefs.HasKey(entry.name + "Gotten") && PlayerPrefs.GetInt(entry.name + "Gotten") == 1)
            {
                PlayerPrefs.SetInt(entry.name + "Gotten", 0);
            }
        }
    }
}
