using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovingState : NPCState
{
    public Node currentNode;
    public List<Node> path = new List<Node>();

    public float moveSpeed = 5f;
    public bool shouldMove = false;

    public override void EnterState(NPC npc, Player player)
    {
        if (Vector3.Distance(player.gameObject.transform.position, npc.gameObject.transform.position) <= 2.5f)
        {
            Unit npcUnit = npc.GetComponent<Unit>();
            Unit playerUnit = player.GetComponent<Unit>();

            playerUnit.currentHP -= npcUnit.damage;


            npc.SwitchState(npc.npcIdleState);
        }
        else
        {
            shouldMove = true;
        }
    }

    public override void UpdateState(NPC npc)
    {
        //Node[] nodes = npc.nodes;
        //
        //while (path == null || path.Count == 0)
        //{
        //    path = AStarManager.Instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length)]);
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    shouldMove = true;
        //}
        currentNode = npc.currentNode;
        path = npc.path;

        CreatePath(npc);
    }

    public override void OnCollisionEnter2D(NPC npc)
    {

    }

    public void CreatePath(NPC npc)
    {
        if (shouldMove == true)
        {
            if (path.Count > 0)
            {
                foreach (Node node in path)
                {
                    int i = 0;

                    if (i > 0)
                    {
                        path.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                int x = 0;
                npc.transform.position = Vector3.MoveTowards(npc.transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), moveSpeed * Time.deltaTime);

                if (Vector2.Distance(npc.transform.position, path[x].transform.position) < 0.1f)
                {
                    currentNode = path[x];
                    path.RemoveAt(x);

                    shouldMove = false;

                    npc.SwitchState(npc.npcIdleState);
                }
            }
        }
    }

    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.tag == "Node")
    //    {
    //        currentNode = collider.gameObject.GetComponent<Node>();
    //    }
    //}
}
