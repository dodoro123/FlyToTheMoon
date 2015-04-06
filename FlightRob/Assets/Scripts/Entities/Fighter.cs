using UnityEngine;
using System.Collections;

public class Fighter : Entity {


	WeaponPlatform 			m_platform;
	Weapon 					m_weapon;
	FRCharactorBehaviour 	m_behaviour;
	FRCharactorController	m_controller;
	// Use this for initialization
	void Start () 
	{
		m_behaviour = gameObject.GetComponent<FRCharactorBehaviour>();
		m_platform = gameObject.AddComponent<WeaponPlatform>();
		m_controller = gameObject.AddComponent<FRCharactorController>();


		m_platform.PlacePlatform(new Vector3(0.5f,0,0));
		Object gun = Resources.Load("Entities/Gun");
		GameObject weapon = (GameObject)GameObject.Instantiate(gun,Vector3.zero,Quaternion.identity);
		m_weapon = weapon.GetComponent<Weapon>();
		m_platform.AttachWeaponToSlot(m_weapon);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_behaviour.IsFiring())
		{
			m_weapon.Fire();
		}
	}

	public Vector3 GetVelocity()
	{
		return m_controller.m_velocity;
	}
}
