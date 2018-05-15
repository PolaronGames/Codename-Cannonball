using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	public GameObject wave;
	public float waveDensity = 0.01f;
	int width = 1200/8;
	int height = 1200/8;

	void Start () {
		for(int j = 0; j < height; j++)
		{
			for(int i = 0; i < width; i++)
			{
				float random = Random.Range(0.0f, 1.0f);
				if(random < waveDensity)
				{
					Vector3 position = new Vector3(i, j, 20.0f);
					Instantiate(wave, position, Quaternion.identity);
				}
			}
		}
	}
	

	void Update () {
		
	}
}
