using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour {

	// Tiles
	Tilemap tilemap;
	Tile waterTile;
	Tile sandTile;
	Tile grassTile;
	Vector3Int tilePosition;
	public Sprite water;
	public Sprite sand;
	public Sprite grass;

	// Terrain Parameters
	const int detailLevel = 6;
	public float sandHeight = 0.8f;
	public float grassHeight = 0.85f;

	// Heightmap data
	static int n = (int)Math.Pow(2,detailLevel) + 1;
	static float[,] map = new float[n,n];
	System.Random random;

	// Use this for initialization
	void Start () {
		// Initialize variables
		random = new System.Random ();
		tilemap = this.GetComponents<Tilemap>()[0];
		waterTile = new Tile ();
		sandTile = new Tile ();
		grassTile = new Tile ();
		waterTile.sprite = water;
		sandTile.sprite = sand;
		grassTile.sprite = grass;
		// Generate heightmap
		GenTerrain();
		// Set tiles based on heightmap
		Init ();
	}

	void Init()
	{
		for (int i = 0; i < n; i++) {
			int y = i - n / 2;
			for (int j = 0; j < n; j++) {
				int x = j - n / 2;
				tilePosition = new Vector3Int (x, y, 0);
				if (map [j, i] >= sandHeight && map [j, i] < grassHeight) {
					tilemap.SetTile (tilePosition, sandTile);
				} else if (map [j, i] >= grassHeight) {
					tilemap.SetTile (tilePosition, grassTile);
				} else {
					tilemap.SetTile (tilePosition, waterTile);
				}
			}
		}
	}

	float fRand(float minimum, float maximum)
	{ 
		return (float)random.NextDouble() * (maximum - minimum) + minimum;
	}

	void GenTerrain() {
		
		float minElev = 0.0f;
		float maxElev = 0.0f;
		float range = 1.0f;
		for(int dx = n-1; dx >= 2; range /= 2.0f)
		{
        	// Diamond step
			for(int x1 = 0; x1 < n-1; x1 += dx)
	    	{
	    		for(int x2 = 0; x2 < n-1; x2 += dx)
	    		{
					// Average of corners
	    			float avg = 0.25f*(map[x1,x2] +
						map[x1+dx,x2] +
						map[x1,x2+dx] +
						map[x1+dx,x2+dx]);
					
	    			// Set center node value
					map[x1+dx/2,x2+dx/2] = avg + fRand(-range, range);

					// Save max and min for normalization
					if(map[x1+dx/2,x2+dx/2] < minElev)
					{
						minElev = map[x1+dx/2,x2+dx/2];
					}
					else if(map[x1+dx/2,x2+dx/2] > maxElev)
					{
						maxElev = map[x1+dx/2,x2+dx/2];
					}
	    		}
        	}
			dx /= 2;
        	// Square step
			for(int x1 = 0; x1 < n-1; x1 += dx)
	    	{
	    		for(int x2 = (x1+dx)%(2*dx); x2 < n -1; x2 += 2*dx)
	    		{
					// Average of corners
	    			float avg = 0.25f*(map[(x1-dx+n-1)%(n-1),x2] +
	    			map[(x1+dx)%(n-1),x2] +
	    			map[x1,(x2+dx)%(n-1)] +
	    			map[x1,(x2-dx+n-1)%(n-1)]);

					// Set center node value
	    			map[x1,x2] = avg + fRand(-range, range);

					// Edge cases
	    			if(x1 == 0) map[n-1,x2] = avg;
	    			if(x2 == 0) map[x1,n-1] = avg;

					// Save max and min for normalization
					if(map[x1,x2] < minElev)
					{
						minElev = map[x1,x2];
					}
					else if(map[x1,x2] > maxElev)
					{
						maxElev = map[x1,x2];
					}
	    	}
        }
    }
		// Normalize heights
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				map [j, i] -= minElev;
				map [j, i] /= (maxElev - minElev);
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
