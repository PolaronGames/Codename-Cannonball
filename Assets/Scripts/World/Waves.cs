using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	public GameObject wave;
	public float waveDensity = 0.01f;
	int width = 1200/8;
	int height = 1200/8;
	GameObject water;

	void Start () {
		water = GameObject.FindGameObjectWithTag("Water");
		for(int j = 0; j < height; j++)
		{
			for(int i = 0; i < width; i++)
			{
				float random = Random.Range(0.0f, 1.0f);
				if(random < waveDensity)
				{
					Vector3 position = new Vector3(i, j, 20.0f);
					GameObject wavePrefab = Instantiate(wave, position, Quaternion.identity);
					wavePrefab.transform.parent = water.transform;
				}
			}
		}
	}
	

	void Update () {
		
	}
}
