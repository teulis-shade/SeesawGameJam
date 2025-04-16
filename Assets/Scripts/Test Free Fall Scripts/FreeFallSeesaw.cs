using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FreeFallSeesaw : MonoBehaviour
{

    public FreeFallPlayer playerLeft;
    public FreeFallPlayer playerRight;
    public Side sideDown = Side.LEFT;

    public enum Side
    {
        LEFT, RIGHT 
    };


    // Transfer velocity from player A to player B
    void TransferPlayerVelocity(FreeFallPlayer playerA, FreeFallPlayer playerB)
    {
        double velocity = math.abs(playerA.velocity) * math.pow(playerA.mass / playerB.mass, 0.5);
        //double velocity = math.abs(playerA.velocity) * playerA.mass / playerB.mass;
        Debug.Log($"Applying velocity {velocity}");
        playerB.ApplyInstantVelocity(velocity);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        FreeFallPlayer hittingPlayer = collision.gameObject.GetComponent<FreeFallPlayer>();
        if (hittingPlayer == playerLeft)
        {
            if (sideDown == Side.RIGHT) {
                Debug.LogFormat($"Left player hit the seesaw with {collision.relativeVelocity.y} velocity");
                TransferPlayerVelocity(hittingPlayer, playerRight);
                sideDown = Side.LEFT;
            }
            hittingPlayer.StopMovementOnContact(collision.contacts[0].point.y);
        }
        if (hittingPlayer == playerRight)
        {
            if (sideDown == Side.LEFT) {
                Debug.Log($"Right player hit the seesaw with {collision.relativeVelocity.y} velocity");
                TransferPlayerVelocity(hittingPlayer, playerLeft);
                sideDown = Side.RIGHT;
            }
            hittingPlayer.StopMovementOnContact(collision.contacts[0].point.y);
        }
    }
}
