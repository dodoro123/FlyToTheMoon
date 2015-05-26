using UnityEngine;
using System.Collections;

public class Weapon : Entity {

	Entity m_owner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetupWeapon(Entity owner)
	{
		m_owner = owner;
	}

	public void Fire()
	{
		Quaternion forward = Quaternion.LookRotation(transform.forward,Vector3.up);
		GameObject bullet = (GameObject)GameObject.Instantiate(Resources.Load("Entities/Bullet"),transform.position+2*transform.forward,forward);
		Bullet bulletCom = bullet.GetComponent<Bullet>();
		bulletCom.Init(m_owner);
	}
}
