using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicLevelManager : Manager<DynamicLevelManager> {

	List<LevelComponent> 	m_components = new List<LevelComponent>();
	LevelComponent			m_current;
	LevelComponent			m_next;
    LevelComponent          m_previous;
	bool connected=false;
    Vector3                 m_lastPlayerPosition;
    bool                    m_contained;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_components.Count>2)
		{
			PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();
            //enter next block
			if(m_next.m_bouds.Contains(fighter.transform.position - m_next.transform.position))
			{
				LevelComponent candidate=null;
				for(int i=0;i<m_components.Count;i++)
				{
					if(m_components[i]!=m_current&&m_components[i]!=m_next && m_components[i] != m_previous)
					{
						candidate = m_components[i];
						break;
					}
				}
                m_previous = m_current;
				m_current = m_next;
				m_next = candidate;
                ConnectAfter(m_current,m_next);
				OnEnterLevel(m_current,m_next);
				//UnityEditor.EditorApplication.isPaused = true;
			}
            //enter previous block
            if (m_previous&&m_previous.m_bouds.Contains(fighter.transform.position - m_previous.transform.position))
            {
                LevelComponent candidate = null;
                for (int i = 0; i < m_components.Count; i++)
                {
                    if (m_components[i] != m_current && m_components[i] != m_next && m_components[i] != m_previous)
                    {
                        candidate = m_components[i];
                        break;
                    }
                }
                m_next = m_current;
                m_current = m_previous;
                m_previous = candidate;
                ConnectBefore(m_current, m_previous);
                OnEnterLevel(m_current, m_previous);
                //UnityEditor.EditorApplication.isPaused = true;
            }

            if (m_current.m_bouds.Contains(fighter.transform.position - m_current.transform.position))
            {
                m_contained = true;
            }
            else
            {
                if (m_contained && fighter.transform.position.y > m_lastPlayerPosition.y)
                {
                    OnFlyToTheMoon(); 
                    Debug.Log("fly to the moon!!!!!!!!!!!!!!!");
                }
                m_contained = false;
            }

            m_lastPlayerPosition = fighter.transform.position;
            Debug.Log("contained:"+m_contained);
        }
	}
    void OnFlyToTheMoon()
    {
        //load university 
        LevelLoadingManager.m_singleton.LoadLevel(2);
    }
    void OnEnterLevel(LevelComponent _current,LevelComponent _next)
	{
		//PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();

		//EntityManager.m_singleton.RequestEntity(EntityType.Helicopter,AIServiceManager.m_singleton.GetPredictPlayerPosition(5),Quaternion.Euler(0,-90,0));
		//EntityManager.m_singleton.RequestEntity(EntityType.Tank, AIServiceManager.m_singleton.GetPredictPlayerPosition(5), Quaternion.Euler(0,-90,0));
	}

	void ConnectAfter(LevelComponent current,LevelComponent after)
	{
		after.transform.position = current.m_center+new Vector3(current.m_bouds.extents.x+after.m_bouds.extents.x,0,0)-after.m_bouds.center;
		Debug.DrawLine(current.m_center,after.m_center,Color.black,1);
	}
    void ConnectBefore(LevelComponent current, LevelComponent before)
    {
        before.transform.position = current.m_center - new Vector3(current.m_bouds.extents.x + before.m_bouds.extents.x, 0, 0) - before.m_bouds.center;
        Debug.DrawLine(current.m_center, before.m_center, Color.black, 1);

    }


    public void RegisterLevelComponent(LevelComponent subLevel,bool starter=false)
	{
		if(starter)
		{
			m_next = subLevel;
            m_previous = subLevel;
		}
		m_components.Add(subLevel);

	}
	public void UnregisterLevelComponent(LevelComponent subLevel)
	{
		m_components.Remove(subLevel);
	}
    public Vector3 GetCurrentCenter()
    {
        return m_current.m_bouds.center;
    }
    public Vector3 GetForward()
    {
        return m_next.m_center - m_current.m_center;
    }
    public Vector3 GetBackward()
    {
        return m_previous.m_center - m_current.m_center;
    }
}
