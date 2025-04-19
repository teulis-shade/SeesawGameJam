using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameController;

public class NearestArrowsManager : MonoBehaviour
{

    public List<NearestArrow> nearestArrows;
    private GameController gc;
    //public NearestArrow[] nearestArrow;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();

        for (int i = 0; i < transform.childCount; i++)
        {
            nearestArrows.Add(transform.GetChild(i).GetComponent<NearestArrow>());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float find = gc.activePlayer.gameObject.transform.position.x;
        //set flyingObject of arrows
        List<FlyingObject> yesFlyList = gc.GetFlyingObjects();

        int low = 0;
        int height = yesFlyList.Count;

        int min = 0;
        int max = yesFlyList.Count - 1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (find == yesFlyList[mid].height)
            {
                //return ++mid;
                mid += 1;
                break;
            }
            else if (find < yesFlyList[mid].height)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }

        //mid has my array value
        //find nearest x
        //nearestArrows
        //get nearest things








    }
}
