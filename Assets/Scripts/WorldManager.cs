using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    WorldState currentState;

    public PlayerTurnState playerTurnState = new PlayerTurnState();
    public NPCTurnState npcTurnState = new NPCTurnState();
    public MoveAllState moveAllState = new MoveAllState();

    public NPC[] npcs;
    public Player player;

    void Start()
    {
        npcs = FindObjectsOfType<NPC>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        currentState = playerTurnState;

        currentState.EnterState(this, npcs);
    }

    public void SwitchState(WorldState state)
    {
        currentState = state;
        state.EnterState(this, npcs);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this);
    }

    void Update()
    {
        currentState.UpdateState(this, player);
    }
}
