using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public WeaponSlotToChange weaponSlot;
	public enum WeaponSlotToChange{
		WEAPON_SLOT_ONE,
		WEAPON_SLOT_TWO
	}
	// Use this for initialization
	void Start () {
		weaponSlot = WeaponSlotToChange.WEAPON_SLOT_ONE;
		// Set indicator at top of menu for which weapon is being changed
		// Highlight currently selected Weapon for Weapon Slot One
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1) && weaponSlot == WeaponSlotToChange.WEAPON_SLOT_TWO)
		{
			weaponSlot = WeaponSlotToChange.WEAPON_SLOT_ONE;
			// Set indicator at top of menu for which weapon is being changed
			// Highlight currently selected Weapon for Weapon Slot One
		}
		if (Input.GetKey(KeyCode.Alpha2) && weaponSlot == WeaponSlotToChange.WEAPON_SLOT_ONE)
		{
			weaponSlot = WeaponSlotToChange.WEAPON_SLOT_TWO;
			// Set indicator at top of menu for which weapon is being changed
			// Highlight currently selected Weapon for Weapon Slot Two
		}
	}
}
