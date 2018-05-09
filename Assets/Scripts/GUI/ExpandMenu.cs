using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ExpandMenu : MonoBehaviour {

	public bool isMenuOpen;
	GameObject islandMenuPanel;

	

	// Use this for initialization
	public void toggleIslandMenu(){
		isMenuOpen = !isMenuOpen;
		islandMenuPanel.SetActive(isMenuOpen);
	}

	void Start(){
		islandMenuPanel = GameObject.FindGameObjectWithTag("islandMenu");
		islandMenuPanel.SetActive(false);
	}
}
 