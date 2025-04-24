using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorFunctions
{
    [MenuItem("EditorFunctions/Copy FlyingObjects into Journal")]
    public static void MakeJournalEntries()
    {
        GameController gc = GameObject.FindObjectOfType<GameController>();
        JournalCheck journal = GameObject.FindObjectOfType<JournalCheck>();
        foreach (GameController.FlyingObjectContainer obj in gc.flyingObjectInit)
        {
            journal.journalEntries.Add(new JournalCheck.JournalEntry(obj.obj.GetComponent<FlyingObject>().objectName, obj.obj.GetComponent<FlyingObject>().description, obj.obj.GetComponent<SpriteRenderer>().sprite));
        }
    }

    [MenuItem("EditorFunctions/Clear Journal Memory")]
    public static void ClearJournal()
    {
        GameObject.FindObjectOfType<JournalCheck>().ClearEntries();
    }
}
