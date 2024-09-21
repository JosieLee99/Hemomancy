using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldState
{
    public abstract void EnterState(WorldManager worldManager, NPC[] npcs);

    public abstract void UpdateState(WorldManager worldManager, Player player);

    public abstract void OnCollisionEnter2D(WorldManager worldManager);
}
