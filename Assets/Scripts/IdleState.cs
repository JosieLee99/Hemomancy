using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    public override void EnterState(Player player)
    {
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState(Player player)
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Debug.Log("Entering Idle State");
            player.SwitchState(player.movingState);
        }
    }

    public override void OnCollisionEnter2D(Player player)
    {

    }
}
