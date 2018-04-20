using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipInventory : MonoBehaviour {

	public int cannonBall;
	public int pineapple;
	public int doubloons;

    public void updateCannonballValue(int modifyByValue){
        cannonBall += modifyByValue;
        Debug.Log("Cannonball count: " + cannonBall.ToString());
    }

    public void updatePineappleValue(int modifyByValue){
        pineapple += modifyByValue;
        Debug.Log("Pineapple Count: " + pineapple.ToString());
    }

    public void updateDoubloonLevel(int modifyByValue){
        doubloons += modifyByValue;
        Debug.Log("Doubloon Count: " + doubloons.ToString());
    }
}
