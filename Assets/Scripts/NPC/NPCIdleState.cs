using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    public override void EnterState(NPC npc, Player player, WorldManager worldManager)
    {
        //Debug.Log("NPC:IDLE");
    }

    public override void UpdateState(NPC npc, WorldManager worldManager)
    {

    }

    public override void OnCollisionEnter2D(NPC npc)
    {

    }
}
