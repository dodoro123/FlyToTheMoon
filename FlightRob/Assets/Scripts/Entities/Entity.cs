using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {


	public EntityType m_type;
	void Awake()
	{
		EntityManager.Singleton.Register(this);
	}

    virtual public void OnAwake()
    { }


	void OnDestroy()
	{
		EntityManager.Singleton.Unregister(this);
	}

}
