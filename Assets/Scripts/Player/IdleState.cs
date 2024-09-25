using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    public override void EnterState(Player player)
    {

    }

    public override void UpdateState(Player player, WorldManager worldManager)
    {
        SetDirectionWithoutMovement(player);

        //if (worldManager.currentState == worldManager.playerTurnState)
        //{
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    player.SwitchState(player.movingState);
                }
            }
        //}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            player.SwitchState(player.attackState);
        }
    }

    public override void OnCollisionEnter2D(Player player)
    {

    }

    public void SetDirectionWithoutMovement(Player player)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.directionFaced = 2;  //Top
                Debug.Log("Top");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                player.directionFaced = 4;  //Right
                Debug.Log("Right");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                player.directionFaced = 6;  //Bottom
                Debug.Log("Bottom");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                player.directionFaced = 8;  //Left
                Debug.Log("Left");
            }
        }
    }
}
