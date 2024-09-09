using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class NoiseGrid : MonoBehaviour
{
    public GameObject player;

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

    public Tile grass;
    public Tile stone;
    public Tile water;

    public Tilemap tilemap;

    void Start()
    {
        if (!PlayerPrefs.HasKey("WorldGenerated"))
        {
            MakeNoiseGrid(densityPercOne);
            Smooth(iterations);
            MakePonds(densityPercTwo);

            PlayerPrefs.SetInt("WorldGenerated", 1);
        }
        else
        {
            tileTest.Load();
        }

        player.transform.position = new Vector3(mapWidth / 2, mapHeight / 2, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MakeNoiseGrid(densityPercOne);
            Smooth(iterations);
            MakePonds(densityPercTwo);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            DestroyMap();
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
                    tilemap.SetTile(new Vector3Int(i, j, 0), grass);
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
                                if (testedTile == grass)
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
                        tilemap.SetTile(new Vector3Int(i, j, 0), grass);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), stone);
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
                if(tilemap.GetTile(new Vector3Int(i, j, 0)) == grass)
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

    public void DestroyMap()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }
}
