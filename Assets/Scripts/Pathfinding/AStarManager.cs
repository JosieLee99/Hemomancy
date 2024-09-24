using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AStarManager : MonoBehaviour
{
    public static AStarManager Instance;

    public NoiseGrid noiseGrid;

    [SerializeField] private int gridWidth = 100;
    [SerializeField] private int gridHeight = 100;
    [SerializeField] private float cellWidth = 1f;
    [SerializeField] private float cellHeight = 1f;
    [SerializeField] private bool visualiseGrid = true;
    //[SerializeField] private bool showTexts;

    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    private Dictionary<Vector2, Cell> availableCells;

    [SerializeField] public List<Vector2> availableVectors;

    [SerializeField] public List<Vector2> cellsToSearch;
    [SerializeField] public List<Vector2> searchedCells;
    [SerializeField] public List<Vector2> finalPath;
    [SerializeField] public List<Vector2> finalPathTemp;

    [SerializeField] public NPC[] npcs;

    public Vector2 pos;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        npcs = FindObjectsOfType<NPC>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (availableVectors.Count > 0)
            {
                availableVectors.Clear();
            }

            GenerateGrid();

            foreach (NPC npc in npcs)
            {
                npc.path = RandomizePath(new Vector2(npc.gameObject.transform.position.x, npc.gameObject.transform.position.y));
            }

            //int randStart = Random.Range(0, availableVectors.Count);

            //RandomizePath(new Vector2(availableVectors[randStart].x, availableVectors[randStart].y));
        }

        //if (availableVectors.Count > 0)
        //{
        //    availableVectors.Clear();
        //}

        //GenerateGrid();
    }

    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();

        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }

        for(int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (noiseGrid.tilemap.GetTile(new Vector3Int(i, j, 0)) != noiseGrid.grass1 && noiseGrid.tilemap.GetTile(new Vector3Int(i, j, 0)) != noiseGrid.grass2 && noiseGrid.tilemap.GetTile(new Vector3Int(i, j, 0)) != noiseGrid.grass3)
                {
                    Debug.Log("THIS IS A WALL");
                    pos = new Vector2(i, j);
                    cells[pos].isWall = true;
                }
                else
                {
                    pos = new Vector2(i, j);
                    availableVectors.Add(pos);
                }
            }
        }
    }

    public List<Vector2> RandomizePath(Vector2 startPos)
    {
        //foreach (KeyValuePair<Vector2, Cell> cell in cells)
        //{
        //    if (!cell.Value.isWall)
        //    {
        //        Vector2 cellPos = new Vector2(cell.Key.x, cell.Key.y);
        //        
        //        availableCells.Add(cellPos, new Cell(cellPos));
        //
        //        availableVectors.Add(cellPos);
        //    }
        //}

            //int randStart = Random.Range(0, availableVectors.Count);
            int randEnd = Random.Range(0, availableVectors.Count);

            //FindPath(new Vector2(availableVectors[randStart].x, availableVectors[randStart].y), new Vector2(availableVectors[randEnd].x, availableVectors[randEnd].y));
            FindPath(startPos, new Vector2(availableVectors[randEnd].x, availableVectors[randEnd].y));

            //for (int i = finalPathTemp.Count; i > 0; i--)
            //{
            //    Debug.Log("Hello");
            //    finalPath.Add(finalPathTemp[i]);
            //}

            finalPath.Reverse();

            return finalPath;
    }


    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        searchedCells = new List<Vector2>();
        cellsToSearch = new List<Vector2>() { startPos };
        finalPath = new List<Vector2>();

        Cell startCell = cells[startPos];
        startCell.gCost = 0;
        startCell.hCost = GetDistance(startPos, endPos);
        startCell.fCost = GetDistance(startPos, endPos);

        while(cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];

            foreach(Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if(c.fCost < cells[cellToSearch].fCost || 
                    c.fCost == cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }

            cellsToSearch.Remove(cellToSearch);
            searchedCells.Add(cellToSearch);

            if(cellToSearch == endPos)
            {
                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    finalPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                finalPath.Add(startPos);
                return;
            }

            SearchCellNeighbors(cellToSearch, endPos);
        }
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for(float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbor = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbor < cells[neighborPos].gCost)
                    {
                        Cell neighborNode = cells[neighborPos];

                        neighborNode.connection = cellPos;
                        neighborNode.gCost = GcostToNeighbor;
                        neighborNode.hCost = GetDistance(neighborPos, endPos);
                        neighborNode.fCost = neighborNode.gCost + neighborNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private int GetDistance(Vector3 pos1, Vector2 pos2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    private void OnDrawGizmos()
    {
        if (!visualiseGrid || cells == null)
        {
            return;
        }

        foreach (KeyValuePair<Vector2, Cell> kvp in cells)
        {
            if (!kvp.Value.isWall)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }

            if (finalPath.Contains(kvp.Key))
            {
                Gizmos.color = Color.magenta;
            }

            //float gizmoSize = showTexts ? 0.2f : 1;

            Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth, cellHeight));
        }
    }

}

public class Cell
{
    public Vector2 position;
    public int fCost = int.MaxValue;
    public int gCost = int.MaxValue;
    public int hCost = int.MaxValue;
    public Vector2 connection;
    public bool isWall;

    public Cell(Vector2 pos)
    {
        position = pos;
    }
}


