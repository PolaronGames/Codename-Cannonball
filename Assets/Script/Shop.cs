using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	int cannonballPurchaseAmount;
	InputField cannonballInputField;
	public void ConfirmPurchase(){
		
	}

	void getCannonballPurchaseAmount(){
		cannonballInputField = GameObject.FindGameObjectWithTag("cannonballPurchaseTextField").GetComponent<InputField>();
		cannonballPurchaseAmount = Convert.ToInt32(cannonballInputField.text);
	}
}
