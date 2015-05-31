using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EntityType
{
	Helicopter,
	Tank,
}


public class InstantiateManager : Manager<InstantiateManager>
{
	List<Entity> m_entityPool;
	Dictionary<EntityType,string> m_entityDic = new Dictionary<EntityType, string>();
	// Use this for initialization
	void Start () 
	{
		m_entityDic.Add(EntityType.Helicopter,"Entities/Fighter_lvl2");
		m_entityDic.Add(EntityType.Tank,"Entities/Tank");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public Entity Instantiate(EntityType _type, Vector3 _pos,Quaternion _rot)
	{
		string res=null;
		m_entityDic.TryGetValue(_type,out res);
		if(res!=null)
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(res),_pos,_rot);
			return (Entity)obj.GetComponent(typeof(Entity));
		}
		return null;
	}
}
