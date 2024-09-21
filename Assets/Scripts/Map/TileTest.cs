using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileTest : MonoBehaviour
{
    public TileBase grassTile1;
    public TileBase grassTile2;
    public TileBase grassTile3;
    public TileBase grassTile4;

    public Tile stoneTile;
    public Tile stoneBottomTile;
    public Tile stoneRightTile;
    public Tile stoneLeftTile;
    public Tile stoneBottomRightTile;
    public Tile stoneBottomLeftTile;
    public Tile stoneBottomSingleTile;
    public Tile stoneStripVerticalTile;
    public Tile stoneLeftCornerTile;
    public Tile stoneRightCornerTile;
    public Tile stoneLeftStraightRightCornerTile;
    public Tile stoneRightStraightLeftCornerTile;

    public TileBase waterTile;

    public Tilemap terrainMap;
    public List<TileBase> tiles = new List<TileBase>();
    public List<int> xPositionTile = new List<int>();
    public List<int> yPositionTile = new List<int>();

    public int i;

    public void Awake()
    {
        ES3AutoSaveMgr.Current.Load();
        Load();
    }

    void OnApplicationQuit()
    {
        ES3AutoSaveMgr.Current.Save();

        Save();
    }

    public void Update()
    {
        if (ES3.KeyExists("tiles"))
        {
            tiles = (List<TileBase>)ES3.Load("tiles");
        }
        if (ES3.KeyExists("yPositionTile"))
        {
            yPositionTile = (List<int>)ES3.Load("yPositionTile");
        }
        if (ES3.KeyExists("xPositionTile"))
        {
            xPositionTile = (List<int>)ES3.Load("xPositionTile");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Load();
        }
    }

    public void Save()
    {
        if (ES3.KeyExists("tiles"))
        {
            ES3.DeleteKey("tiles");
        }
        if (ES3.KeyExists("xPositionTile"))
        {
            ES3.DeleteKey("xPositionTile");
        }
        if (ES3.KeyExists("yPositionTile"))
        {
            ES3.DeleteKey("yPositionTile");
        }

        tiles.Clear();
        xPositionTile.Clear();
        yPositionTile.Clear();

        Tilemap tilemap = GetComponent<Tilemap>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int gridPlace = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = tilemap.GetTile(gridPlace);
            if (tile != null)
            {
                
                #region Add Specific Tile
                
                if (tile.name == "Grass1")
                {
                    tiles.Add(grassTile1);
                }
                if (tile.name == "Grass2")
                {
                    tiles.Add(grassTile2);
                }
                if (tile.name == "Grass3")
                {
                    tiles.Add(grassTile3);
                }
                if (tile.name == "Grass4")
                {
                    tiles.Add(grassTile4);
                }
                if (tile.name == "Stone")
                {
                    tiles.Add(stoneTile);
                }
                if (tile.name == "Stone_Left")
                {
                    tiles.Add(stoneLeftTile);
                }
                if (tile.name == "Stone_Right")
                {
                    tiles.Add(stoneRightTile);
                }
                if (tile.name == "Stone_Bottom")
                {
                    tiles.Add(stoneBottomTile);
                }
                if (tile.name == "Stone_Bottom_Left")
                {
                    tiles.Add(stoneBottomLeftTile);
                }
                if (tile.name == "Stone_Bottom_Right")
                {
                    tiles.Add(stoneBottomRightTile);
                }
                if (tile.name == "Stone_Bottom_Single")
                {
                    tiles.Add(stoneBottomSingleTile);
                }
                if (tile.name == "Stone_Strip_Vertical")
                {
                    tiles.Add(stoneStripVerticalTile);
                }
                if (tile.name == "Stone_Left_Corner")
                {
                    tiles.Add(stoneLeftCornerTile);
                }
                if (tile.name == "Stone_Right_Corner")
                {
                    tiles.Add(stoneRightCornerTile);
                }
                if (tile.name == "Stone_LeftSraight_RightCorner")
                {
                    tiles.Add(stoneLeftStraightRightCornerTile);
                }
                if (tile.name == "Stone_RightSraight_LeftCorner")
                {
                    tiles.Add(stoneRightStraightLeftCornerTile);
                }
                if (tile.name == "Water")
                {
                    tiles.Add(waterTile);
                }

                #endregion

                xPositionTile.Add(pos.x);
                yPositionTile.Add(pos.y);
            }
        }

        ES3.Save("tiles", (List<TileBase>)tiles);
        ES3.Save("xPositionTile", (List<int>)xPositionTile);
        ES3.Save("yPositionTile", (List<int>)yPositionTile);
    }

    public void Load()
    {
        i = 0;

        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;

        if (ES3.KeyExists("tiles"))
        {
            tiles = (List<TileBase>)ES3.Load("tiles");
        }
        if (ES3.KeyExists("yPositionTile"))
        {
            yPositionTile = (List<int>)ES3.Load("yPositionTile");
        }
        if (ES3.KeyExists("xPositionTile"))
        {
            xPositionTile = (List<int>)ES3.Load("xPositionTile");
        }

        foreach (var tile in tiles)
        {
            Vector3Int position = new Vector3Int(xPositionTile[i], yPositionTile[i], 0);

            terrainMap.SetTile(position, tile);

            i++;
        }
    }
}