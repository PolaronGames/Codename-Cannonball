using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignWeapon : MonoBehaviour {

	public void onClick()
	{
		Player player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
		ExpandMenu menu = GameObject.FindGameObjectWithTag("islandMenu").GetComponent<ExpandMenu>();
		Text name = this.GetComponentInChildren<Text>();
		player.weaponSlotOneName = name.text;

		if (menu.weaponSlot == ExpandMenu.WeaponSlotToChange.WEAPON_SLOT_ONE)
		{
			player.weaponSlotOneName = name.text;
			// Highlight Selected Weapon
		}
		else if (menu.weaponSlot == ExpandMenu.WeaponSlotToChange.WEAPON_SLOT_TWO)
		{
			player.weaponSlotTwoName = name.text;
			// Highlight Selected Weapon
		}
	}
	
}
