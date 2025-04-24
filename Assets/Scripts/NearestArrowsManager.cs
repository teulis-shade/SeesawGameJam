using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static GameController;

public class NearestArrowsManager : MonoBehaviour
{

    public List<NearestArrow> nearestArrows;
    public int arrowNum;
    [SerializeField] GameObject prefabNearestArrow;
    private GameController gc;
    //public NearestArrow[] nearestArrow;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();

        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            nearestArrows.Add(transform.GetChild(i).GetComponent<NearestArrow>());
        }*/


        
        nearestArrows = new List<NearestArrow>();
        for (int i = 0; i < arrowNum; i+=1)
        {
            NearestArrow arrow = Instantiate(prefabNearestArrow, transform).GetComponent<NearestArrow>();
            nearestArrows.Add(arrow);
        }
        
        /*
        foreach (FlyingObjectContainer obj in flyingObjectInit)
        {
            for (int i = 0; i < obj.amount; i++)
            {
                FlyingObject flyer = Instantiate(prefabNearestArrow).GetComponent<NearestArrow>();
                flyer.height = Random.Range(obj.heightRange.minimum, obj.heightRange.maximum);
                flyingObjects.Add(flyer);
                flyer.Initialize();
            }
        }
        flyingObjects.Sort((obj1, obj2) => obj1.height.CompareTo(obj2.height));
        */
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 playerPos = gc.activePlayer.transform.position;
        //transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);

        float find = gc.activePlayer.gameObject.transform.position.y;
        //set flyingObject of arrows
        List<FlyingObject> yesFlyList = gc.GetFlyingObjects();
        int height = yesFlyList.Count;

        int min = 0;
        int max = yesFlyList.Count - 1;
        int ret_mid = -1;
        while (min <= max)
        //while (min < max)
        {
            int mid = (min + max) / 2;
            if (find == yesFlyList[mid].height)
            {
                //return ++mid;
                mid += 1;
                ret_mid = mid;
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
        //if (min == max)
        if (min > max)
        {
            ret_mid = max;

            //fix bug where it does not grab min if first value is gone
            if ((ret_mid == -1) && (yesFlyList.Count > 0))
            {
                ret_mid = 0;
            }
 
            //ret_mid = min;
        }


        //mid has my array value
        //find nearest x
        //nearestArrows
        //get nearest things



        if (ret_mid != -1)
        {
            //PART 1: RUN LOOP TO GET arrowNum NEW CLOSEST FLYING OBJECTS

            //VERSION 2: get nearest based on player direction
            List<FlyingObject> newClosestFlyingObjects = new List<FlyingObject>();
            int i = ret_mid;
            int j = ret_mid;
            while (((i > -1) && (j < height)) && (newClosestFlyingObjects.Count < arrowNum))
            {
                if (Mathf.Sign((float) gc.activePlayer.velocity) < 0) {
                    //we going down baby
                    newClosestFlyingObjects.Add(yesFlyList[i]);
                    i -= 1;
                }
                else
                {
                    //we goin up baby
                    newClosestFlyingObjects.Add(yesFlyList[j]);
                    j += 1;
                }

                //try 1
                /*
                if (Mathf.Sign((float)gc.activePlayer.velocity) > 0)
                {
                    //we going down baby
                    newClosestFlyingObjects.Add(yesFlyList[i]);
                    i += 1;
                }
                else
                {
                    //we goin up baby
                    newClosestFlyingObjects.Add(yesFlyList[j]);
                    j -= 1;
                }*/

            }

            //VERSION 1: get all nearest flying objects
            /*
            List<FlyingObject> newClosestFlyingObjects = new List<FlyingObject>();
            int i = ret_mid;
            int j = ret_mid;
            while (((i > -1) || (j < height)) && (newClosestFlyingObjects.Count < arrowNum))
            {
                if (i == j)
                {
                    newClosestFlyingObjects.Add(yesFlyList[i]);
                    i -= 1;
                    j += 1;
                }
                else
                {
                    //CHOOSE BETWEEN I AND J
                    
                    float arrowBelow;
                    if (i > -1){
                        arrowBelow = MathF.Abs(yesFlyList[i].transform.position.y - find);
                    }else{
                        //REACH BELOW LIMIT
                        //get above forehead
                        newClosestFlyingObjects.Add(yesFlyList[j]);
                        j += 1;
                        continue;
                    }
                    float arrowAbove;
                    if (j < height)
                    {
                        arrowAbove = MathF.Abs(yesFlyList[j].transform.position.y - find);
                    }else{
                        //arrowAbove = float.NaN;

                        //REACH ABOVE LIMIT
                        newClosestFlyingObjects.Add(yesFlyList[i]);
                        i -= 1;
                        continue;
                    }


                    if (arrowAbove <= arrowBelow)
                    {
                        newClosestFlyingObjects.Add(yesFlyList[j]);
                        //get j
                        j += 1;
                    }
                    else
                    {
                        //get i
                        newClosestFlyingObjects.Add(yesFlyList[i]);
                        i -= 1;
                    }
                }
            }*/

            //PART 2

            //GET FLYING OJBECTS TO ASSIGN
            //At the end of this
            //ALL NearestArrow in nearestArrows that are not in newClosestFlyingObjects will be null
            //AND newClosestFlyingObjects WILL only hold unassigned Flying Objects
            foreach (NearestArrow na in nearestArrows)
            {
                FlyingObject toCheck = na.GetFlyingObject();
                if (toCheck != null)
                {
                    //if object is in other list
                    if (newClosestFlyingObjects.Contains(toCheck))
                    {
                        newClosestFlyingObjects.Remove(toCheck);
                    }
                    else
                    {
                        na.SetFlyingObject(null);
                    }
                }
            }

            //ASSIGN ALL ARROWS WITH LEFTOVER FLYING OBJECTS LIST
            foreach (NearestArrow na in nearestArrows)
            {
                FlyingObject toCheck = na.GetFlyingObject();
                if (toCheck == null)
                {
                    if (newClosestFlyingObjects.Count > 0)
                    {
                        na.SetFlyingObject(newClosestFlyingObjects[0]);
                        newClosestFlyingObjects.RemoveAt(0);
                    }
                }
            }
        }







    }
}
