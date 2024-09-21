using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    public override void EnterState(NPC npc, Player player)
    {
        //Debug.Log("NPC:IDLE");
    }

    public override void UpdateState(NPC npc)
    {

    }

    public override void OnCollisionEnter2D(NPC npc)
    {

    }
}
