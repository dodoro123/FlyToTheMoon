using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : Manager<EntityManager>
{
	//List<Type> m_entityType = new List<Type>();
	List<Entity> m_entityList = new List<Entity>();
	PlayerFighter m_fighter;
	Dictionary<EntityType,List<Entity>> m_pool = new Dictionary<EntityType, List<Entity>>();
	Dictionary<EntityType,List<Entity>> m_used = new Dictionary<EntityType, List<Entity>>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Register(Entity entity)
	{
		if(!m_entityList.Contains(entity))
		{
			m_entityList.Add(entity);
			//if(!m_entityType.Contains(entity.GetType()))
			//{
			//	m_entityType.Add(entity.GetType());
			//}
			if(entity is PlayerFighter)
				m_fighter = entity as PlayerFighter;
		}
	}
	public void Unregister(Entity entity)
	{
		m_entityList.Remove(entity);
	}
	public PlayerFighter GetPlayerFighter()
	{
		return m_fighter;
	}
	public Entity RequestEntity(EntityType _type,Vector3 _pos,Quaternion _rot)
	{
		List<Entity> list;
		m_pool.TryGetValue(_type, out list);
		Entity result = null;
		if(list == null)
		{
			list = new List<Entity>();
			m_pool.Add(_type,list);
			result = InstantiateManager.m_singleton.Instantiate(_type,_pos,_rot);
			result.m_type = _type;
			SafeAddTo(m_used,_type,result);
		}
		else
		{
			if(list.Count!=0)
			{
				result = list[0];
				result.transform.position = _pos;
				result.transform.rotation = _rot;
				SafeAddTo(m_used,_type,result);
				list.Remove(result);
			}
			else
			{
				result = InstantiateManager.m_singleton.Instantiate(_type,_pos,_rot);
				result.m_type = _type;
				SafeAddTo(m_used,_type,result);
			}
		}
		return result;
	}
	public void RealseEntity(EntityType _type, Entity _entity)
	{
		List<Entity> list;
		m_used.TryGetValue(_type, out list);
		if(list == null)
		{
			Debug.LogError("try to lease empty type. type:"+_type);
		}
		else
		{
			if(list.Count!=0)
			{
				for(int i=0;i<list.Count;i++)
				{
					if(list[i]==_entity)
					{
						//todo hide it
						_entity.transform.position = new Vector3(1000,1000,1000);
						SafeAddTo(m_pool,_type,_entity);
					}
				}
			}
			else
			{
				Debug.LogError("try to lease type:"+_type+" which have 0 in used");
			}
		}
	}
	void SafeAddTo(Dictionary<EntityType,List<Entity>> _dic,EntityType _type,Entity _entity)
	{
		List<Entity> list ;
		_dic.TryGetValue(_type,out list);
		if(list == null)
		{
			list = new List<Entity>();
			list.Add(_entity);
			_dic.Add(_type,list);
		}
		else
		{
			list.Add(_entity);
		}
	}
}
