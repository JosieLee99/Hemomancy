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
        Debug.Log("Entering Move State");
        movePoint = player.movePoint;
        movePoint.parent = null;
        whatStopsMovement = player.whatStopsMovement;
    }

    public override void UpdateState(Player player)
    {
        player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(player.gameObject.transform.position, movePoint.position) <= .05f)
        {
            if (Input.GetAxisRaw("Horizontal") >= .05f || Input.GetAxisRaw("Horizontal") <= -.05f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    isMoving = true;
                }
            }
            
            if (Input.GetAxisRaw("Vertical") >= .05f || Input.GetAxisRaw("Vertical") <= -.05f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    isMoving = true;
                }
            }
        }

        if(isMoving == true && Vector3.Distance(player.gameObject.transform.position, movePoint.position) <= .05f)
        {
            isMoving = false;
        
            player.SwitchState(player.idleState);
        }
    }

    public override void OnCollisionEnter2D(Player player)
    {

    }
}
