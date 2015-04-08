﻿using UnityEngine;
using System.Collections;

public class WeaponPlatform : Entity 
{

	Vector3 		m_weaponSlot = new Vector3(0,0,0);
	Quaternion 		m_weaponRot;

	void Start () 
	{
		
	}
	public void AttachWeaponToSlot(Weapon weapon)
	{
		weapon.gameObject.transform.parent = this.gameObject.transform;
		weapon.gameObject.transform.localPosition = m_weaponSlot;
		weapon.gameObject.transform.localRotation = m_weaponRot;
	}
	public void PlacePlatform(Vector3 pos,Quaternion rot)
	{
		m_weaponSlot = pos;
		m_weaponRot = rot;
	}
}
