using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState
{
    public abstract void EnterState(NPC npc, Player player);

    public abstract void UpdateState(NPC npc);

    public abstract void OnCollisionEnter2D(NPC npc);
}
