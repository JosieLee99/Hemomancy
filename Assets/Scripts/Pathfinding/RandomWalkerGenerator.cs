using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalkerGenerator : MonoBehaviour
{
    //public Tilemap tilemap;
    //public NoiseGrid noiseGrid;
    //
    //public Grid[,] grid;
    //
    //public int mapWidth;
    //public int mapHeight;
    //
    //public Node nodePrefab;
    //public List<Node> nodeList = new List<Node>();
    //
    //public void CreateNodes()
    //{
    //    //int count = 0;
    //    //int j = 1;
    //
    //    for (int x = 0; x < mapWidth; x++)
    //    {
    //        for (int y = 0; y < mapHeight; y++)
    //        {
    //            if (tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass1 || tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass2 || tilemap.GetTile(new Vector3Int(x, y, 0)) == noiseGrid.grass3)
    //            {
    //                Node node = Instantiate(nodePrefab, new Vector2(x, y), Quaternion.identity);
    //                nodeList.Add(node);
    //
    //                //if (count >= 2)
    //                //{
    //                //    if (Vector2.Distance(previousNode.transform.position, node.transform.position) <= 1.5f)
    //                //    {
    //                //        ConnectNodes(previousNode, node);
    //                //        ConnectNodes(node, previousNode);
    //                //    }
    //                //}
    //                //else
    //                //{
    //                //    previousNode = node;
    //                //}
    //                //
    //                //count++;
    //            }
    //        }
    //    }
    //    CreateConnections();
    //}
    //
    //public void CreateConnections()
    //{
    //    for (int i = 0; i < nodeList.Count; i++)
    //    {
    //        for (int j = i + 1; j < nodeList.Count; j++)
    //        {
    //            if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.5f)
    //            {
    //                ConnectNodes(nodeList[i], nodeList[j]);
    //                ConnectNodes(nodeList[j], nodeList[i]);
    //            }
    //        }
    //    }
    //}
    //
    //void ConnectNodes(Node from, Node to)
    //{
    //    if (from == to) { return; }
    //
    //    from.connections.Add(to);
    //}
    //
    //public void DestroyNodes()
    //{
    //    foreach (Node node in nodeList)
    //    {
    //        Destroy(node.gameObject);
    //    }
    //
    //    GameObject[] colliders = GameObject.FindGameObjectsWithTag("WallCollider");
    //
    //    foreach(GameObject collider in colliders)
    //    {
    //        Destroy(collider);
    //    }
    //
    //    nodeList.Clear();
    //}
}