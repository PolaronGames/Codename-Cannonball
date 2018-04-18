using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Player data
	Transform position;
	public float speed;

	// Use this for initialization
	void Start () {
		position = this.GetComponentsInParent<Transform>() [1];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("right"))
		{
			Vector3 step = new Vector3(0.1f*speed, 0.0f, 0.0f);
			position.Translate (step);
		}
		if (Input.GetKey ("left"))
		{
			Vector3 step = new Vector3(-0.1f*speed, 0.0f, 0.0f);
			position.Translate (step);
		}
		if (Input.GetKey ("up"))
		{
			Vector3 step = new Vector3(0.0f, 0.1f*speed, 0.0f);
			position.Translate (step);
		}
		if (Input.GetKey ("down"))
		{
			Vector3 step = new Vector3(0.0f, -0.1f*speed, 0.0f);
			position.Translate (step);
		}
	}
}
