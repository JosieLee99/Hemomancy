using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState
{
    public abstract void EnterState(NPC npc, Player player, WorldManager worldManager);

    public abstract void UpdateState(NPC npc, WorldManager worldManager);

    public abstract void OnCollisionEnter2D(NPC npc);
}
