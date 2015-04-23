﻿using UnityEngine;
using System.Collections;

public class Fighter : Entity {

	WeaponPlatform 			m_platform;
	Weapon 					m_weapon;
	Controller	m_controller;
	// Use this for initialization
	void Start () 
	{
		m_platform = gameObject.AddComponent<WeaponPlatform>();
		m_controller = (Controller)gameObject.GetComponent(typeof(Controller));
		
		
		m_platform.PlacePlatform(new Vector3(0,-0.5f,0),transform.localRotation);
		Object gun = Resources.Load("Entities/Gun");
		GameObject weapon = (GameObject)GameObject.Instantiate(gun,Vector3.zero,Quaternion.identity);
		m_weapon = weapon.GetComponent<Weapon>();
		m_platform.AttachWeaponToSlot(m_weapon);
		Quaternion forward = Quaternion.LookRotation(transform.forward,Vector3.up);
		m_weapon.transform.rotation=forward;
		m_weapon.SetupWeapon(this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.frameCount%10==0)
		{
			m_weapon.Fire();
		}
	}
	
	public Vector3 GetVelocity()
	{
		return m_controller.velocity;
	}
}
