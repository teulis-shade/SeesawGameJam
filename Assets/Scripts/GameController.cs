using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<FlyingObject> flyingObjects;
    public PlayerScript activePlayer;
    public CameraController camera;
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
        activePlayer.StartMovement(10000d);
    }

    public void GameOver()
    {
        DisplayEndScreen();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        activePlayer.transform.position += new Vector3(horizontalMovement, 0f) / 5f;
    }

    private void DisplayEndScreen()
    {
        //Display Max Height
        //Display End Mass
        //Display Cause of Death
        //Display Buttons
        // *End Score possibly*
    }

    public FlyingObject[] CheckCollision(double prevHeight, double nextHeight, double position)
    {
        bool inRange = false;
        List <FlyingObject> hitObjects = new();
        for (int i = 0; i < flyingObjects.Count; i++)
        {
            if (flyingObjects[i].height < prevHeight)
            {
                inRange = true;
                continue;
            }
            else if (inRange && flyingObjects[i].height > prevHeight)
            {
                inRange = false;
                break;
            }
            else
            {
                if (flyingObjects[i].CheckCollision(position))
                {
                    hitObjects.Add(flyingObjects[i]);
                }
            }
        }
        return hitObjects.ToArray();
    }

    public void InitializeObjects()
    {
        flyingObjects = new List<FlyingObject>();
        foreach (FlyingObjectContainer obj in flyingObjectInit)
        {

        }
    }

    public void UpdateCamera(double height)
    {
        camera.UpdateCamera(height);
    }
}
