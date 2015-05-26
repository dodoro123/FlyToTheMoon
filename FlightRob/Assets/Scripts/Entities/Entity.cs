using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public EntityType m_type;
	void Awake()
	{
		EntityManager.Singleton.Register(this);
	}

	void OnDie()
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		Bullet bullet = collision.gameObject.GetComponent<Bullet>();
		if(bullet&&bullet.m_owner is Fighter )
		{
			//do nothing
		}
		else
		{
			EntityManager.m_singleton.RealseEntity(m_type,this);
			OnDie ();
		}
	}

	void OnDestroy()
	{
		EntityManager.Singleton.Unregister(this);
	}
}
