using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : Manager<EntityManager>
{
	//List<Type> m_entityType = new List<Type>();
	List<Entity> m_entityList = new List<Entity>();
	PlayerFighter m_fighter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Register(Entity entity)
	{
		m_entityList.Add(entity);
		//if(!m_entityType.Contains(entity.GetType()))
		//{
		//	m_entityType.Add(entity.GetType());
		//}
		if(entity is PlayerFighter)
			m_fighter = entity as PlayerFighter;

	}
	public void Unregister(Entity entity)
	{
		m_entityList.Remove(entity);
	}
	public PlayerFighter GetPlayerFighter()
	{
		return m_fighter;
	}
}
