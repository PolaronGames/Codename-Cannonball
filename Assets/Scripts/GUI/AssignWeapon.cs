using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignWeapon : MonoBehaviour {

	public void onClick()
	{
		Movement playerData = GameObject.FindGameObjectWithTag("player").GetComponent<Movement>();
		Weapon weaponScript = playerData.GetComponent<Weapon>();
		Menu menu = GameObject.FindGameObjectWithTag("islandMenu").GetComponent<Menu>();
		Text name = this.GetComponentInChildren<Text>();

		if (menu.weaponSlot == Menu.WeaponSlotToChange.WEAPON_SLOT_ONE)
		{
			playerData.weaponSlotOneName = name.text;
			weaponScript.SetActiveWeapon();
			// Highlight Selected Weapon
		}
		else if (menu.weaponSlot == Menu.WeaponSlotToChange.WEAPON_SLOT_TWO)
		{
			playerData.weaponSlotTwoName = name.text;
			weaponScript.SetActiveWeapon();
			// Highlight Selected Weapon
		}
	}
	
}
