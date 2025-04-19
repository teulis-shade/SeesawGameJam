using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<FlyingObject> flyingObjects;
    public PlayerScript activePlayer;
    public CameraController cam;
    public List<FlyingObjectContainer> flyingObjectInit;

    [System.Serializable]
    public class Range
    {
        public float minimum;
        public float maximum;
    }
    [System.Serializable]
    public class FlyingObjectContainer
    {
        public GameObject obj;
        public Range heightRange;
        public int amount;
    }

    public void StartGame()
    {
        InitializeObjects();
        activePlayer.StartMovement(100d);
    }

    public void GameOver()
    {
        DisplayEndScreen();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        activePlayer.transform.position += new Vector3(horizontalMovement, 0f) / 5f;
        activePlayer.transform.position = new Vector3(Mathf.Clamp(activePlayer.transform.position.x, -19f, 19f), activePlayer.transform.position.y);
    }

    private void DisplayEndScreen()
    {
        //Display Max Height
        //Display End Mass
        //Display Cause of Death
        //Display Buttons
        // *End Score possibly*
    }

    public FlyingObject[] CheckCollision(double lowerHeight, double upperHeight, double positionLeft, double positionRight)
    {
        bool inRange = false;
        List <FlyingObject> hitObjects = new();
        for (int i = 0; i < flyingObjects.Count; i++)
        {
            // if (!inRange && flyingObjects[i].height > prevHeight)
            // {
            //     inRange = true;
            // }
            // if (inRange && flyingObjects[i].height > nextHeight)
            // {
            //     inRange = false;
            //     break;
            // }
            // if (inRange && flyingObjects[i].CheckCollision(positionLeft, positionRight))
            // {
            //     hitObjects.Add(flyingObjects[i]);
            // }
            double objectHeight = flyingObjects[i].height;
            if (upperHeight > objectHeight)
            {
                break;
            }
            if (upperHeight > objectHeight && objectHeight > lowerHeight)
            {
                hitObjects.Add(flyingObjects[i]);
            }
        }
        return hitObjects.ToArray();
    }

    public void InitializeObjects()
    {
        flyingObjects = new List<FlyingObject>();
        foreach (FlyingObjectContainer obj in flyingObjectInit)
        {
            for (int i = 0; i < obj.amount; i++)
            {
                FlyingObject flyer = Instantiate(obj.obj).GetComponent<FlyingObject>();
                flyer.height = Random.Range(obj.heightRange.minimum, obj.heightRange.maximum);
                flyingObjects.Add(flyer);
                flyer.Initialize();
            }
        }
        flyingObjects.Sort((obj1, obj2) => obj1.height.CompareTo(obj2.height));
    }

    public void RemoveFlyer(FlyingObject obj)
    {
        flyingObjects.Remove(obj);
    }

    public void UpdateCamera(double height)
    {
        cam.UpdateCamera(height);
    }
}
