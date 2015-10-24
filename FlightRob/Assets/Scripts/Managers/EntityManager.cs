using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public delegate void RequestEntityCallBack(Entity entity);
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
			//m_entityList.Add(entity);
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
		//m_entityList.Remove(entity);
	}
	public PlayerFighter GetPlayerFighter()
	{
		return m_fighter;
	}
    public Entity GetRandomEnemy(Entity my)
    {
        Entity _enemy=null;
        for(int i=0;i<m_entityList.Count;i++)
        {
            if(m_entityList[i]!=my)
            {
                _enemy = m_entityList[i];
            }
        }
        return _enemy;
    }

    public Entity GetRandomEntity(EntityType _type)
    {
        List<Entity> list;
        m_used.TryGetValue(_type, out list);
        if (list != null&& list.Count>0)
        {
            int index = Random.Range(0, list.Count - 1);
            for (int i = 0; i < list.Count; i++)
            {
                LiveEntity live = list[(index + i) % list.Count] as LiveEntity;
                if (live != null && !live.m_dieing)
                {
                    return list[(index + i)%list.Count];
                }
            }
        }
        return null;
    }

    public int GetEntityNum(EntityType _type)
    {
        List<Entity> list;
        m_used.TryGetValue(_type, out list);
        if(list!=null)
        {
            return list.Count;
        }
        return 0;
    }
    public int GetEntityNumAll()
    {
        int num=0;
        foreach(var list in m_used.Values)
        {
            num += list.Count;
        }
        return num;
    }
    public Entity RequestEntity(EntityType _type,Vector3 _pos,Quaternion _rot, RequestEntityCallBack callback =null)
	{
		List<Entity> list;
		m_pool.TryGetValue(_type, out list);
		Entity result = null;
		if(list == null)
		{
			list = new List<Entity>();
			m_pool.Add(_type,list);
			result = InstantiateManager.m_singleton.Instantiate(_type,_pos,_rot);
            result.OnAwake();
            result.m_type = _type;
            SafeAddTo(m_used,_type,result);
		}
		else
		{
			if(list.Count!=0)//reusing entitys
			{
				result = list[0];
                result.OnAwake();
                result.gameObject.SetActive(true);
                Renderer[] renders = result.GetComponentsInChildren<MeshRenderer>();
				foreach(var render in renders)
				{
					render.enabled = true;
				}
				result.transform.position = _pos;
				result.transform.rotation = _rot;
				SafeAddTo(m_used,_type,result);
				list.Remove(result);
			}
			else
			{
 				result = InstantiateManager.m_singleton.Instantiate(_type,_pos,_rot);
                result.OnAwake();
                result.m_type = _type;
                result.name += m_used[_type].Count;
                SafeAddTo(m_used,_type,result);
			}
		}
        if(callback!=null)
            callback(result);
        m_entityList.Add(result);
		return result;
	}
	public void RealseEntity(EntityType _type, Entity _entity)
	{
        Debug.Log("RealseEntity");
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
                        _entity.gameObject.SetActive(false);
                        SafeAddTo(m_pool,_type,_entity);
						list.Remove(_entity);
                        Debug.Log("lease type:" + _type +":"+ _entity.name);
                    }
				}
			}
			else
			{
				Debug.LogError("try to lease type:"+_type+":"+ _entity.name + " which have 0 in used");
			}
		}
        m_entityList.Remove(_entity);
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
