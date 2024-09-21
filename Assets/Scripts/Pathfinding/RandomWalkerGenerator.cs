using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalkerGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public NoiseGrid noiseGrid;
    
    //public enum Grid
    //{
    //    Floor,
    //    Wall,
    //    Empty
    //}

    public Grid[,] grid;

    //public List<Walker> walkers;
    //public Tilemap floorMap;
    //public Tilemap wallMap;

    //public Tile floor;
    //public Tile wall;

    public int mapWidth;
    public int mapHeight;

    //public int maxWalkers = 10;
    //public int tileCount = default;
    //public float fillPercent = 0.5f;

    public Node nodePrefab;
    public List<Node> nodeList;

    //public NPC_Controller npc;

    //private bool canDrawGizmos;

    public void CreateNodes()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass1 || tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass2 || tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass3)
                {
                    Node node = Instantiate(nodePrefab, new Vector2(x, y), Quaternion.identity);
                    nodeList.Add(node);
                }
            }
        }
        CreateConnections();
    }

    public void CreateConnections()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = i + 1; j < nodeList.Count; j++)
            {
                if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.5f)
                {
                    ConnectNodes(nodeList[i], nodeList[j]);
                    ConnectNodes(nodeList[j], nodeList[i]);
                }
            }
        }
        //canDrawGizmos = true;
        //SpawnAI();
    }

    void ConnectNodes(Node from, Node to)
    {
        if (from == to) { return; }

        from.connections.Add(to);
    }

    public void DestroyNodes()
    {
        foreach(Node node in nodeList)
        {
            Destroy(node.gameObject);
        }

        GameObject[] colliders = GameObject.FindGameObjectsWithTag("WallCollider");

        foreach(GameObject collider in colliders)
        {
            Destroy(collider);
        }

        nodeList.Clear();
    }

    //void SpawnAI()
    //{
    //    Node randNode = nodeList[Random.Range(0, nodeList.Count)];
    //
    //    NPC_Controller newNPC = Instantiate(npc, randNode.transform.position, Quaternion.identity);
    //
    //    newNPC.currentNode = randNode;
    //}

    //private void OnDrawGizmos()
    //{
    //     if (canDrawGizmos)
    //     {
    //         Gizmos.color = Color.blue;
    //         for(int i =0; i < nodeList.Count; i++)
    //         {
    //             for(int j = 0; j < nodeList[i].connections.Count; j++)
    //             {
    //                 Gizmos.DrawLine(nodeList[i].transform.position, nodeList[i].connections[j].transform.position);
    //             }
    //         }
    //     }
    //}
}

//public class Walker
//{
//    public Vector2 pos;
//    public Vector2 dir;
//    public float chanceToChange;
//
//    public Walker(Vector2 p, Vector2 d, float c)
//    {
//        pos = p;
//        dir = d;
//        chanceToChange = c;
//    }
//}