using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int cannonBall;
	public int pineapple;
	public int doubloons;

	// Use this for initialization
	void Start () {
		cannonBall = 50;
		pineapple = 10;
		doubloons = 1000;
	}

    public void ModifyCannonballValue(int modifyByValue){
        cannonBall += modifyByValue;
        Debug.Log(cannonBall.ToString());
    }

    public void ModifyPineappleValue(int modifyByValue){
        pineapple += modifyByValue;
        Debug.Log(pineapple.ToString());
    }

	public void BuyCannonball(int buyQuantity){
		cannonBall += buyQuantity;
		Debug.Log(cannonBall.ToString());
    }

    public void BuyPineapple(int buyQuantity)
    {
        pineapple += buyQuantity;
        Debug.Log(pineapple.ToString());

    }

}

