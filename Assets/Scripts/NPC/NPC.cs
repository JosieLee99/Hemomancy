using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    NPCState currentState;

    public NPCIdleState npcIdleState = new NPCIdleState();
    public NPCMovingState npcMovingState = new NPCMovingState();
    public NPCAttackState npcAttackState = new NPCAttackState();

    public Node currentNode;
    public List<Node> path = new List<Node>();

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

        Node[] nodes = FindObjectsOfType<Node>();

        while (path == null || path.Count == 0)
        {
            path = AStarManager.Instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length - 1)]);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Node")
        {
            currentNode = collider.gameObject.GetComponent<Node>();
        }
    }
}
