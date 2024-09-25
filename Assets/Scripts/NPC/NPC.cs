using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    NPCState currentState;

    public NPCIdleState npcIdleState = new NPCIdleState();
    public NPCMovingState npcMovingState = new NPCMovingState();
    public NPCAttackState npcAttackState = new NPCAttackState();

    public List<Vector2> path = new List<Vector2>();

    public WorldManager worldManager;

    public Player player;

    void Start()
    {
        worldManager = GameObject.FindAnyObjectByType<WorldManager>();
        player = GameObject.FindObjectOfType<Player>();

        currentState = npcIdleState;

        currentState.EnterState(this, player, worldManager);
    }

    public void SwitchState(NPCState state)
    {
        currentState = state;
        state.EnterState(this, player, worldManager);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this);
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this, worldManager);
    }
}
