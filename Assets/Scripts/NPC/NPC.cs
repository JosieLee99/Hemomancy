using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    NPCState currentState;

    public NPCIdleState npcIdleState = new NPCIdleState();
    public NPCMovingState npcMovingState = new NPCMovingState();
    public NPCAttackState npcAttackState = new NPCAttackState();

    //public Node currentNode;
    public List<Vector2> path = new List<Vector2>();

    public WorldManager worldManager;

    public Player player;

    void Start()
    {
        worldManager = GameObject.FindAnyObjectByType<WorldManager>();
        player = GameObject.FindObjectOfType<Player>();

        currentState = npcIdleState;

        currentState.EnterState(this, player);
    }

    public void SwitchState(NPCState state)
    {
        currentState = state;
        state.EnterState(this, player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this);
    }

    void Update()
    {
        currentState.UpdateState(this);

        //Node[] nodes = FindObjectsOfType<Node>();
    }

    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.tag == "Node")
    //    {
    //        currentNode = collider.gameObject.GetComponent<Node>();
    //    }
    //}
}
