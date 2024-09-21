using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState
{
    public AttacksList attacksList;
    public Player playerRef;

    public Unit playerUnit;

    public override void EnterState(Player player)
    {
        attacksList = GameObject.FindObjectOfType<AttacksList>();
    }

    public override void UpdateState(Player player, WorldManager worldManager)
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            attacksList.InstantiateAttack(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attacksList.InstantiateAttack(2);
        }
    }

    public override void OnCollisionEnter2D(Player player)
    {

    }
}
