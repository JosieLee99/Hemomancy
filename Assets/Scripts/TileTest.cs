using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileTest : MonoBehaviour
{
    public TileBase stoneTile;
    public TileBase grassTile;
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
                Debug.Log("x:" + pos.x + " y:" + pos.y + " tile:" + tile.name);
                
                #region Add Specific Tile
                
                if (tile.name == "Grass")
                {
                    tiles.Add(grassTile);
                }
                if (tile.name == "Stone")
                {
                    tiles.Add(stoneTile);
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

        Debug.Log("Saved");
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

        Debug.Log("Loaded");
    }
}