using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : ShipInventory {

    public GameObject doubloonTextObject;
    Text doubloonTextValue;

	// Use this for initialization
	void Start () {
		cannonBall = 50;
		pineapple = 10;
		doubloons = 1000;
        doubloonTextObject = GameObject.FindGameObjectWithTag("doubloonCount");
        doubloonTextValue = doubloonTextObject.GetComponent<Text>();
        doubloonTextValue.text = doubloons.ToString();
	}

    public void buyCannonball(int buyQuantity)
    {
        if(doubloons >= 5){
        cannonBall += buyQuantity;
        updateDoubloonLevel(buyQuantity * -5);
        }
        Debug.Log("Cannonball count: " + cannonBall.ToString());
    }

    public void buyPineapple(int buyQuantity)
    {
        pineapple += buyQuantity;
        Debug.Log("Cannonball count: " + pineapple.ToString());
    }

    public new void updateDoubloonLevel(int modifyByValue)
    {
        doubloons += modifyByValue;
        doubloonTextValue.text = doubloons.ToString();
        Debug.Log("Doubloon Count: " + doubloons.ToString());
    }

}

