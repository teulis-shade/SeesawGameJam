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
        public string desription;
        public int gotten;//0 -> false, 1 -> true
        public Sprite Sprite;

        public void SetAchievemnt(int gotten){
            this.gotten = gotten; 
        }
    }


    //public string[] achievements;
    //[System.Serializable] Achievement[] achievements;
    [SerializeField] Achievement[] achievements;






    // Start is called before the first frame update
    void Start()
    {
        foreach (Achievement ach in achievements)
        {
            //if (HasKey(ach.key))
            if (PlayerPrefs.HasKey(ach.name))
            {
                ach.SetAchievemnt(Getint(ach.name));
            }
            //gotten
            //print(value);
        }
        /*
        foreach (float value in values)
        {
            print(value);
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    public void HasKey(string KeyName)
    {
        if (PlayerPrefs.HasKey(KeyName))
        {
            Debug.Log("The key " + KeyName + " exists");
        }
        else
            Debug.Log("The key " + KeyName + " does not exist");
    }
}
