﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignWeapon : MonoBehaviour {

	public void onClick()
	{
		Player player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
		Weapon weaponScript = player.GetComponent<Weapon>();
		Menu menu = GameObject.FindGameObjectWithTag("islandMenu").GetComponent<Menu>();
		Text name = this.GetComponentInChildren<Text>();

		if (menu.weaponSlot == Menu.WeaponSlotToChange.WEAPON_SLOT_ONE)
		{
			player.weaponSlotOneName = name.text;
			weaponScript.SetActiveWeapon();
			// Highlight Selected Weapon
		}
		else if (menu.weaponSlot == Menu.WeaponSlotToChange.WEAPON_SLOT_TWO)
		{
			player.weaponSlotTwoName = name.text;
			weaponScript.SetActiveWeapon();
			// Highlight Selected Weapon
		}
	}
	
}
