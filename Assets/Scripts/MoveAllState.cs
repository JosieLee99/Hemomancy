using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAllState : WorldState
{
    public override void EnterState(WorldManager worldManager, NPC[] npcs)
    {
        foreach (NPC npc in npcs)
        {
            npc.SwitchState(npc.npcMovingState);
        }
    }

    public override void UpdateState(WorldManager worldManager, Player player)
    {
        
    }

    public override void OnCollisionEnter2D(WorldManager worldManager)
    {

    }
}
