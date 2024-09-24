using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class NoiseGrid : MonoBehaviour
{
    public GameObject player;

    public RandomWalkerGenerator randomWalkerGenerator;

    public static NoiseGrid Instance = null;

    public TileTest tileTest;

    public int mapWidth;
    public int mapHeight;

    public int iterations;
    public int grassCount;
    public int stoneCount;
    public int waterCount;

    public int densityPercOne;
    public int densityPercTwo;

    public bool worldGenerated;

    public Tile grass1;
    public Tile grass2;
    public Tile grass3;
    public Tile grass4;

    public Tile black;

    public Tile stone;
    public Tile stoneBottom;
    public Tile stoneRight;
    public Tile stoneLeft;
    public Tile stoneBottomRight;
    public Tile stoneBottomLeft;
    public Tile stoneBottomSingle;
    public Tile stoneStripVertical;
    public Tile stoneLeftCorner;
    public Tile stoneRightCorner;
    public Tile stoneBothCorner;
    public Tile stoneLeftStraightRightCorner;
    public Tile stoneRightStraightLeftCorner;

    public bool tileUp;
    public bool tileRight;
    public bool tileDown;
    public bool tileLeft;

    public GameObject wallCollider;

    public List<Tile> stones = new List<Tile>();

    public Tile water;

    public Tilemap tilemap;

    void Start()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        stones.Add(stone);
        stones.Add(stoneLeft);
        stones.Add(stoneRight);
        stones.Add(stoneBottom);
        stones.Add(stoneBottomRight);
        stones.Add(stoneBottomLeft);
        stones.Add(stoneBottomSingle);
        stones.Add(stoneStripVertical);
        stones.Add(stoneLeftCorner);
        stones.Add(stoneRightCorner);
        stones.Add(stoneLeftStraightRightCorner);
        stones.Add(stoneRightStraightLeftCorner);

        if (!PlayerPrefs.HasKey("WorldGenerated"))
        {
            DestroyMap();

            MakeNoiseGrid(densityPercOne);
            Smooth(iterations);
            MakePonds(densityPercTwo);
            //RedoStone();
            //FinalRevisions();
            InitializeStartingArea();
            RedoStone();
            //FinalRevisions();
            //randomWalkerGenerator.CreateNodes();
            //CreateNodes();
            AddColliders(wallCollider);

            PlayerPrefs.SetInt("WorldGenerated", 1);
        }
        else
        {
            ES3AutoSaveMgr.Current.Load();
            tileTest.Load();

            //RedoStone();
            //FinalRevisions();
            InitializeStartingArea();
            RedoStone();
            //FinalRevisions();
            //randomWalkerGenerator.CreateNodes();
            AddColliders(wallCollider);
        }

        //randomWalkerGenerator = FindObjectOfType<RandomWalkerGenerator>();

        player.transform.position = new Vector3(mapWidth / 2, mapHeight / 2, 0);
    }

    //public void Start()
    //{
    //    GameObject[] wallColliders = GameObject.FindGameObjectsWithTag("DeleteThis");
    //
    //    foreach(GameObject wall in wallColliders)
    //    {
    //        Destroy(wall);
    //    }
    //}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DestroyMap();

            MakeNoiseGrid(densityPercOne);
            Smooth(iterations);
            MakePonds(densityPercTwo);
            //RedoStone();
            //FinalRevisions();
            InitializeStartingArea();
            RedoStone();
            //FinalRevisions();
            //randomWalkerGenerator.CreateNodes();
            //CreateNodes();
            AddColliders(wallCollider);

            PlayerPrefs.SetInt("WorldGenerated", 1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            DestroyMap();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            InitializeStartingArea();
        }
    }

    public void MakeNoiseGrid(int density)
    {
        int[,] noise_grid = new int[mapWidth, mapHeight];
        for(int i = 0; i < mapWidth; i++)
        {
            for(int j = 0; j < mapHeight; j++)
            {
                int random = Random.Range(1, 100);

                if (random > density)
                {
                    int randInt = Random.Range(0, 3);

                    switch (randInt)
                    {
                        case 0:
                            tilemap.SetTile(new Vector3Int(i, j, 0), grass1);
                            break;
                        case 1:
                            tilemap.SetTile(new Vector3Int(i, j, 0), grass2);
                            break;
                        case 2:
                            tilemap.SetTile(new Vector3Int(i, j, 0), grass3);
                            break;
                    }
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), stone);
                }
            }
        }
    }

    public void Smooth(int iterations)
    {
        for (int smoothOnce = 0; smoothOnce < iterations; smoothOnce++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    for (int f = -1; f <= 1; f++)
                    {
                        for (int g = -1; g <= 1; g++)
                        {
                            if (tilemap.GetTile(new Vector3Int(i + f, j + g, 0)) != null)
                            {
                                TileBase testedTile = tilemap.GetTile(new Vector3Int(i + f, j + g, 0));
                                if (testedTile == grass1 || testedTile == grass2 || testedTile == grass3 || testedTile == grass4)
                                {
                                    grassCount++;
                                }
                                if (testedTile == stone)
                                {
                                    stoneCount++;
                                }
                            }
                        }
                    }
                    if (grassCount > stoneCount)
                    {
                        int randInt = Random.Range(0, 3);

                        switch(randInt)
                        {
                            case 0:
                                tilemap.SetTile(new Vector3Int(i, j, 0), grass1);
                                break;
                            case 1:
                                tilemap.SetTile(new Vector3Int(i, j, 0), grass2);
                                break;
                            case 2:
                                tilemap.SetTile(new Vector3Int(i, j, 0), grass3);
                                break;
                        }
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), stone);
                        //Instantiate(wallCollider, new Vector3(i, j, 0), Quaternion.identity);
                    }
                    grassCount = 0;
                    stoneCount = 0;
                }
            }
        }
    }

    public void MakePonds(int density)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                if(tilemap.GetTile(new Vector3Int(i, j, 0)) == grass1 || tilemap.GetTile(new Vector3Int(i, j, 0)) == grass2 || tilemap.GetTile(new Vector3Int(i, j, 0)) == grass3 || tilemap.GetTile(new Vector3Int(i, j, 0)) == grass4)
                {
                    int random = Random.Range(1, 100);

                    if (random > density)
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), water);
                    }
                }
            }
        }
    }

    public void RedoStone()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                foreach (TileBase isStone in stones)
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) == isStone)
                    {
                        tileUp = false;
                        tileRight = false;
                        tileDown = false;
                        tileLeft = false;

                        foreach (TileBase tile in stones)
                        {
                            if (tilemap.GetTile(new Vector3Int(i, j + 1, 0)) == tile && tileUp == false)
                            {
                                tileUp = true;
                            }

                            if (tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == tile && tileRight == false)
                            {
                                tileRight = true;
                            }

                            if (tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == tile && tileDown == false)
                            {
                                tileDown = true;
                            }

                            if (tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == tile && tileLeft == false)
                            {
                                tileLeft = true;
                            }
                        }

                        //Left
                        if (tileUp == true && tileDown == true && tileLeft == false && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneLeft);
                        }

                        //Right
                        else if (tileUp == true && tileDown == true && tileLeft == true && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneRight);
                        }

                        //Upper Left
                        else if (tileUp == false && tileDown == true && tileLeft == false && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneLeft);
                        }

                        //Upper Right
                        else if (tileUp == false && tileDown == true && tileLeft == true && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneRight);
                        }

                        //Bottom Left
                        else if (tileUp == true && tileDown == false && tileLeft == false && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomLeft);
                        }

                        //Bottom Right
                        else if (tileUp == true && tileDown == false && tileLeft == true && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomRight);
                        }

                        //Bottom
                        else if (tileUp == true && tileDown == false && tileLeft == true && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottom);
                        }

                        //Right Isle
                        else if (tileUp == false && tileDown == false && tileLeft == true && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomRight);
                        }

                        //Left Isle
                        else if (tileUp == false && tileDown == false && tileLeft == false && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomLeft);
                        }

                        //Bottom Isle
                        else if (tileUp == true && tileDown == false && tileLeft == false && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomSingle);
                        }

                        //Standalone
                        else if (tileUp == false && tileDown == false && tileLeft == false && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottomSingle);
                        }

                        //Sideways Strip
                        else if (tileUp == false && tileDown == false && tileLeft == true && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBottom);
                        }

                        //Top Down Strip and Top Isle
                        else if (tileUp == true && tileDown == true && tileLeft == false && tileRight == false || tileUp == false && tileDown == true && tileLeft == false && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneStripVertical);
                        }

                        //Middle
                        else
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stone);
                        }

                        //Debug.Log(tilemap.GetTile(new Vector3Int(i + 1, j, 0)));
                    }
                }
            }
        }

        //Pass 2

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                foreach (TileBase isStone in stones)
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) == isStone)
                    {
                        tileUp = false;
                        tileRight = false;
                        tileDown = false;
                        tileLeft = false;
                        
                        foreach (TileBase tile in stones)
                        {
                            if (tilemap.GetTile(new Vector3Int(i, j + 1, 0)) == tile && tileUp == false || tilemap.GetTile(new Vector3Int(i, j + 1, 0)) == black && tileUp == false)
                            {
                                tileUp = true;
                            }
                        
                            if (tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == tile && tileRight == false || tilemap.GetTile(new Vector3Int(i + 1, j , 0)) == black && tileRight == false)
                            {
                                tileRight = true;
                            }
                        
                            if (tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == tile && tileDown == false || tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == black && tileDown == false)
                            {
                                tileDown = true;
                            }
                        
                            if (tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == tile && tileLeft == false || tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == black && tileLeft == false)
                            {
                                tileLeft = true;
                            }
                        }

                        //Left Corner
                        if (tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == stoneBottomRight && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneRight || tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == stoneBottomRight && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomRight || tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == stoneBottom && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomRight || tilemap.GetTile(new Vector3Int(i + 1, j, 0)) == stoneBottom && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneRight)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneLeftCorner);
                        }

                        //Right Corner
                        else if (tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == stoneBottomLeft && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneLeft || tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == stoneBottomLeft && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomLeft || tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == stoneBottom && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomLeft || tilemap.GetTile(new Vector3Int(i - 1, j, 0)) == stoneBottom && tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneLeft)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneRightCorner);
                        }

                        //Both Corner
                        else if (tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomSingle && tileLeft == true && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneBothCorner);
                        }

                        //Left Straight Right Corner
                        else if (tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomSingle && tileLeft == false && tileRight == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneLeftStraightRightCorner);
                        }

                        //Right Straight Left Corner
                        else if (tilemap.GetTile(new Vector3Int(i, j - 1, 0)) == stoneBottomSingle && tileLeft == true && tileRight == false)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), stoneRightStraightLeftCorner);
                        }

                        else if (tileUp == true && tileRight == true && tileDown == true && tileLeft == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), black);
                        }

                        else if (tilemap.GetTile(new Vector3Int(i, j, 0)) == stone && tileUp == true)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), black);
                        }
                    }
                }
            }
        }
    }

    public void InitializeStartingArea()
    {
        for (int i = 47; i <= 53; i++)
        {
            for (int j = 47; j <= 53; j++)
            {
                int randInt = Random.Range(0, 3);

                switch (randInt)
                {
                    case 0:
                        tilemap.SetTile(new Vector3Int(i, j, 0), grass1);
                        break;
                    case 1:
                        tilemap.SetTile(new Vector3Int(i, j, 0), grass2);
                        break;
                    case 2:
                        tilemap.SetTile(new Vector3Int(i, j, 0), grass3);
                        break;
                }
            }
        }
    }

    public void AddColliders(GameObject wallCollider)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                bool addedCollider = false;

                foreach (Tile tile in stones)
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) == tile && addedCollider == false)
                    {
                        Instantiate(wallCollider, new Vector3(i, j, 0), Quaternion.identity);
                        addedCollider = true;
                    }
                }
            }
        }
    }

    //public void CreateNodes()
    //{
    //    for (int i = 0; i < mapWidth; i++)
    //    {
    //        for (int j = 0; j < mapHeight; j++)
    //        {
    //            if(tilemap.GetTile(new Vector3Int(i, j, 0)) == grass1 || tilemap.GetTile(new Vector3Int(i, j, 0)) == grass2 || tilemap.GetTile(new Vector3Int(i, j, 0)) == grass3)
    //            {
    //                Node node = Instantiate(randomWalkerGenerator.nodePrefab, new Vector2(i, j), Quaternion.identity);
    //                randomWalkerGenerator.nodeList.Add(node);
    //            }
    //        }
    //    }
    //
    //    randomWalkerGenerator.CreateConnections();
    //}

    public void DestroyMap()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), null);

                //foreach (Node node in randomWalkerGenerator.nodeList)
                //{
                //    Destroy(node);
                //}
            }
        }

        GameObject[] colliders = GameObject.FindGameObjectsWithTag("WallCollider");

        foreach (GameObject collider in colliders)
        {
            Destroy(collider);
        }

        //randomWalkerGenerator.DestroyNodes();
    }
}
