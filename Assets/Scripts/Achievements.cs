using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Achievements : MonoBehaviour
{
    [System.Serializable]
     public class Achievement
    {
        //public int id;
        //public string key;
        public string name;
        public string description;
        public int gotten = 0;
        public Sprite Sprite;

        public void SetAchievemnt(int gotten){
            this.gotten = gotten;
        }
    }

    public List<Achievement> journalEntries;

    public void UpdateAchievement()
    {

    }
}
