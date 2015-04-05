using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicLevelManager : Manager<DynamicLevelManager> {

	List<LevelComponent> 	m_components = new List<LevelComponent>();
	LevelComponent			m_current;
	LevelComponent			m_next;
	bool connected=false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_components.Count>2)
		{
			Fighter fighter = (Fighter)EntityManager.m_singleton.GetFighter();
			if(m_next.m_bouds.Contains(fighter.transform.position - m_next.transform.position))
			{
				LevelComponent candidate=null;
				for(int i=0;i<m_components.Count;i++)
				{
					if(m_components[i]!=m_current&&m_components[i]!=m_next)
					{
						candidate = m_components[i];
						break;
					}
				}
				m_current = m_next;
				m_next = candidate;
				Connect(m_current,m_next);
				//UnityEditor.EditorApplication.isPaused = true;
			}
		}
	}
	void OnEnterLevel()
	{

	}

	void Connect(LevelComponent before,LevelComponent after)
	{
		after.transform.position = before.m_center+new Vector3(before.m_bouds.extents.x+after.m_bouds.extents.x,0,0)-after.m_bouds.center;
		Debug.DrawLine(before.m_center,after.m_center,Color.black,1);
	}

	public void RegisterLevelComponent(LevelComponent subLevel,bool starter=false)
	{
		if(starter)
		{
			m_next = subLevel;
		}
		m_components.Add(subLevel);

	}
	public void UnregisterLevelComponent(LevelComponent subLevel)
	{
		m_components.Remove(subLevel);
	}
}
