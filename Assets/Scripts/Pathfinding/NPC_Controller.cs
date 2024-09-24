using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Controller : MonoBehaviour
{
    public Node currentNode;
    public NPC npc;

    public bool shouldMove = false;

    public void Update()
    {
        //npc = GameObject.FindObjectsOfType<NPC>();

        //Node[] nodes = FindObjectsOfType<Node>();
        //while (path == null || path.Count == 0)
        //{
        //    path = AStarManager.Instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length)]);
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    shouldMove = true;
        //}

        //CreatePath();
    }

    //public void CreatePath()
    //{
    //    if (shouldMove == true)
    //    {
    //        if (path.Count > 0)
    //        {
    //            foreach (NPC npc in npcs)
    //            {
    //                int i = 0;
    //
    //                if (i > 0)
    //                {
    //                    npc.path.RemoveAt(i);
    //                }
    //                else
    //                {
    //                    i++;
    //                }
    //            }
    //
    //            int x = 0;
    //            transform.position = Vector3.MoveTowards(transform.position, new Vector3(npc.path[x].transform.position.x, path[x].transform.position.y, -2), 3 * Time.deltaTime);
    //
    //            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
    //            {
    //                Debug.Log("1 tile traveled");
    //
    //                currentNode = path[x];
    //                path.RemoveAt(x);
    //
    //                shouldMove = false;
    //            }
    //        }
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Node")
        {
            //currentNode = collider.gameObject.GetComponent<Node>();

            Debug.Log("Node has been set");
        }
    }
}