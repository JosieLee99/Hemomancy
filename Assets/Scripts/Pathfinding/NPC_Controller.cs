using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Controller : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();

    public bool shouldMove = false;

    public void Update()
    {
        Node[] nodes = FindObjectsOfType<Node>();
        while (path == null || path.Count == 0)
        {
            path = AStarManager.Instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length)]);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            shouldMove = true;
        }

        CreatePath();
    }

    public void CreatePath()
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
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), 3 * Time.deltaTime);

                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
                {
                    Debug.Log("1 tile traveled");

                    currentNode = path[x];
                    path.RemoveAt(x);

                    shouldMove = false;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Node")
        {
            currentNode = collider.gameObject.GetComponent<Node>();

            Debug.Log("Node has been set");
        }
    }
}