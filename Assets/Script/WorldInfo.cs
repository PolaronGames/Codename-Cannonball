using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour {

	public int detailLevel = 3;
	public float[,] heightmap;
	public int dockingDistance;
	public float sandHeight = 0.84f;
    public float grassHeight = 0.85f;
    public float rockHeight = 0.9f;
	public Sprite water, sand, grass, rock;
	
	void Start()
	{
		heightmap = this.GetComponentsInChildren<TerrainGenerator>()[0].heightmap;
		this.GetComponentsInChildren<Grid>()[0].cellSize = new Vector3(water.rect.width/100.0f, water.rect.width/100.0f, 0.0f);
		this.GetComponentsInChildren<Player>()[0].tilemap = this.GetComponentsInChildren<TerrainGenerator>()[0].tilemap;
		this.GetComponentsInChildren<Player>()[0].xBlocks = this.GetComponentsInChildren<TerrainGenerator>()[0].xBlocks;
		this.GetComponentsInChildren<Player>()[0].yBlocks = this.GetComponentsInChildren<TerrainGenerator>()[0].yBlocks;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
