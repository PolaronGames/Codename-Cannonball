using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour {

	public int detailLevel = 3;
	public float[,] heightmap;
	
	void Start()
	{
		heightmap = this.GetComponentsInChildren<TerrainGenerator>()[0].heightmap;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
