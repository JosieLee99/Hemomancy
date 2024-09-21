using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CharacterState
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    private bool isMoving;

    public override void EnterState(Player player)
    {
        //Debug.Log("Entering Move State");

        movePoint = player.movePoint;
        movePoint.parent = null;
        whatStopsMovement = player.whatStopsMovement;
    }

    public override void UpdateState(Player player, WorldManager worldManager)
    {
        //SetDirectionWithoutMovement(player);

        player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(player.gameObject.transform.position, movePoint.position) <= .05f)
        {
            if (Input.GetAxisRaw("Horizontal") >= .05f || Input.GetAxisRaw("Horizontal") <= -.05f)
            {
                worldManager.SwitchState(worldManager.moveAllState);

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    SetDirection(player);

                    isMoving = true;
                }
            }
            
            if (Input.GetAxisRaw("Vertical") >= .05f || Input.GetAxisRaw("Vertical") <= -.05f)
            {
                worldManager.SwitchState(worldManager.moveAllState);

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    SetDirection(player);

                    isMoving = true;
                }
            }
        }

        if(isMoving == true && Vector3.Distance(player.gameObject.transform.position, movePoint.position) <= .05f)
        {
            isMoving = false;

            //worldManager.SwitchState(worldManager.moveAllState);

            player.SwitchState(player.idleState);
        }
    }

    public override void OnCollisionEnter2D(Player player)
    {

    }

    public void SetDirection(Player player)
    {
        if (movePoint.position.x < player.transform.position.x && movePoint.position.y > player.transform.position.y)
        {
            player.directionFaced = 1;  //Top Left
            Debug.Log("Top Left");
        }
        else if (movePoint.position.x == player.transform.position.x && movePoint.position.y > player.transform.position.y)
        {
            player.directionFaced = 2;  //Top
            Debug.Log("Top");
        }
        else if (movePoint.position.x > player.transform.position.x && movePoint.position.y > player.transform.position.y)
        {
            player.directionFaced = 3;  //Top Right
            Debug.Log("Top Right");
        }
        else if (movePoint.position.x > player.transform.position.x && movePoint.position.y == player.transform.position.y)
        {
            player.directionFaced = 4;  //Right
            Debug.Log("Right");
        }
        else if (movePoint.position.x > player.transform.position.x && movePoint.position.y < player.transform.position.y)
        {
            player.directionFaced = 5;  //Bottom Right
            Debug.Log("Bottom Right");
        }
        else if (movePoint.position.x == player.transform.position.x && movePoint.position.y < player.transform.position.y)
        {
            player.directionFaced = 6;  //Bottom
            Debug.Log("Bottom");
        }
        else if (movePoint.position.x < player.transform.position.x && movePoint.position.y < player.transform.position.y)
        {
            player.directionFaced = 7;  //Bottom Left
            Debug.Log("Bottom Left");
        }
        else if (movePoint.position.x < player.transform.position.x && movePoint.position.y == player.transform.position.y)
        {
            player.directionFaced = 8;  //Left
            Debug.Log("Left");
        }
        else
        {
            player.directionFaced = 6;  //Bottom
        }
    }

    //public void SetDirectionWithoutMovement(Player player)
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        if (Input.GetKeyDown(KeyCode.W))
    //        {
    //            player.directionFaced = 2;  //Top
    //            Debug.Log("Top");
    //        }
    //        else if (Input.GetKeyDown(KeyCode.D))
    //        {
    //            player.directionFaced = 4;  //Right
    //            Debug.Log("Right");
    //        }
    //        else if (Input.GetKeyDown(KeyCode.S))
    //        {
    //            player.directionFaced = 6;  //Bottom
    //            Debug.Log("Bottom");
    //        }
    //        else if (Input.GetKeyDown(KeyCode.A))
    //        {
    //            player.directionFaced = 8;  //Left
    //            Debug.Log("Left");
    //        }
    //        else
    //        {
    //            player.directionFaced = 6;  //Bottom
    //        }
    //    }
    //}
}
