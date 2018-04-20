using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	InputField cannonballInputField;
	GameObject inventory;
	PlayerInventory inventoryScript;

	public void ConfirmPurchase(){
		inventory = GameObject.FindGameObjectWithTag("inventory");
		inventoryScript = inventory.GetComponent<PlayerInventory>();
		inventoryScript.buyCannonball(getCannonballPurchaseAmount());
	}

	int getCannonballPurchaseAmount(){
		cannonballInputField = GameObject.FindGameObjectWithTag("cannonballPurchaseTextField").GetComponent<InputField>();
		return Convert.ToInt32(cannonballInputField.text);
	}
}
