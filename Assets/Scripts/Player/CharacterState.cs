using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public abstract void EnterState(Player player);

    public abstract void UpdateState(Player player, WorldManager worldManager);

    public abstract void OnCollisionEnter2D(Player player);
}