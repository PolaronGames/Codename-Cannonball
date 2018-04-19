using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{

    // Tiles
    Tilemap tilemap;
    Tile waterTile;
    Tile sandTile;
    Tile grassTile;
    Tile rockTile;
    Vector3Int tilePosition;
    public Sprite water;
    public Sprite sand;
    public Sprite grass;
    public Sprite rock;

    // Terrain Parameters
    const int detailLevel = 6;
    public float sandHeight = 0.84f;
    public float grassHeight = 0.85f;
    public float rockHeight = 0.9f;
    int xBlocks = 50;
    int yBlocks = 50;
    public Texture2D territories;

    // Heightmap data
    static int n = (int)Math.Pow(2, detailLevel) + 1;
    System.Random random;
    Color[] allegiance;
    Color pirate = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    Color greenNavy = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    Color blueNavy = new Color(0.0f, 0.0f, 1.0f, 1.0f);
    Color solus = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    Color empty = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    // Use this for initialization
    void Start()
    {
        // Initialize variables
        random = new System.Random();
        tilemap = this.GetComponents<Tilemap>()[0];
        waterTile = ScriptableObject.CreateInstance<Tile>();
        sandTile = ScriptableObject.CreateInstance<Tile>();
        grassTile = ScriptableObject.CreateInstance<Tile>();
        rockTile = ScriptableObject.CreateInstance<Tile>();
        waterTile.sprite = water;
        sandTile.sprite = sand;
        grassTile.sprite = grass;
        rockTile.sprite = rock;
        // Generate terrain
        allegiance = territories.GetPixels();
        GenTerrain();
    }

    void renderBlock(float[,] block, Vector3Int blockPosition)
    {
        for (int i = 0; i < n; i++)
        {
            int y = i - n / 2;
            for (int j = 0; j < n; j++)
            {
                int x = j - n / 2;
                tilePosition = new Vector3Int(x, y, 0);
                if (block[j, i] >= sandHeight && block[j, i] < grassHeight)
                {
                    tilemap.SetTile(tilePosition + blockPosition, sandTile);
                }
                else if (block[j, i] >= grassHeight && block[j, i] < rockHeight)
                {
                    tilemap.SetTile(tilePosition + blockPosition, grassTile);
                }
                else if (block[j, i] >= rockHeight)
                {
                    tilemap.SetTile(tilePosition + blockPosition, rockTile);
                }
                else
                {
                    tilemap.SetTile(tilePosition + blockPosition, waterTile);
                }
            }
        }
    }

    float fRand(float minimum, float maximum)
    {
        return (float)random.NextDouble() * (maximum - minimum) + minimum;
    }

    float[,] createBlock(int n)
    {
        float[,] map = new float[n, n];
        float minElev = 0.0f;
        float maxElev = 0.0f;
        float range = 1.0f;
        for (int dx = n - 1; dx >= 2; range /= 2.0f)
        {
            // Diamond step
            for (int x1 = 0; x1 < n - 1; x1 += dx)
            {
                for (int x2 = 0; x2 < n - 1; x2 += dx)
                {
                    // Average of corners
                    float avg = 0.25f * (map[x1, x2] +
                        map[x1 + dx, x2] +
                        map[x1, x2 + dx] +
                        map[x1 + dx, x2 + dx]);

                    // Set center node value
                    map[x1 + dx / 2, x2 + dx / 2] = avg + fRand(0.0f, range);

                    // Save max and min for normalization
                    if (map[x1 + dx / 2, x2 + dx / 2] < minElev)
                    {
                        minElev = map[x1 + dx / 2, x2 + dx / 2];
                    }
                    else if (map[x1 + dx / 2, x2 + dx / 2] > maxElev)
                    {
                        maxElev = map[x1 + dx / 2, x2 + dx / 2];
                    }
                }
            }
            dx /= 2;
            // Square step
            for (int x1 = 0; x1 < n - 1; x1 += dx)
            {
                for (int x2 = (x1 + dx) % (2 * dx); x2 < n - 1; x2 += 2 * dx)
                {
                    // Average of corners
                    float avg = 0.25f * (map[(x1 - dx + n - 1) % (n - 1), x2] +
                    map[(x1 + dx) % (n - 1), x2] +
                    map[x1, (x2 + dx) % (n - 1)] +
                    map[x1, (x2 - dx + n - 1) % (n - 1)]);

                    // Set center node value
                    map[x1, x2] = avg + fRand(0.0f, range);

                    // Water on edges
                    if (x1 == 0) map[x1, x2] = 0.0f;
                    if (x2 == 0) map[x1, x2] = 0.0f;
                    if (x1 == n - 2) map[x1, x2] = 0.0f;
                    if (x2 == n - 2) map[x1, x2] = 0.0f;

                    // Save max and min for normalization
                    if (map[x1, x2] < minElev)
                    {
                        minElev = map[x1, x2];
                    }
                    else if (map[x1, x2] > maxElev)
                    {
                        maxElev = map[x1, x2];
                    }
                }
            }
        }
        // Normalize heights
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                map[j, i] -= minElev;
                map[j, i] /= (maxElev - minElev);
            }
        }
        return map;
    }

    void GenTerrain()
    {
        int x, y;
        Color ally;
        Vector3Int blockPosition;
        for (int j = 0; j < yBlocks; j++)
        {
            y = n * j;
            for (int i = 0; i < xBlocks; i++)
            {
                x = n * i;
                ally = allegiance[i + xBlocks * (yBlocks - 1 - j)];
                if (ally == pirate)
                {
                    float[,] block = createBlock(n);
                    blockPosition = new Vector3Int(x, y, 0);
                    renderBlock(block, blockPosition);
                }
                if (ally == greenNavy)
                {
                    float[,] block = createBlock(n);
                    blockPosition = new Vector3Int(x, y, 0);
                    renderBlock(block, blockPosition);
                }
                if (ally == blueNavy)
                {
                    float[,] block = createBlock(n);
                    blockPosition = new Vector3Int(x, y, 0);
                    renderBlock(block, blockPosition);
                }
                if (ally == solus)
                {
                    float[,] block = createBlock(n);
                    blockPosition = new Vector3Int(x, y, 0);
                    renderBlock(block, blockPosition);
                }
				if(ally == empty)
				{
					float[,] block = new float[n,n];
					blockPosition = new Vector3Int(x, y, 0);
					renderBlock(block, blockPosition);
				}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
