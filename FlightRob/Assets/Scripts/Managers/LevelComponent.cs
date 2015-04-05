using UnityEngine;
using System.Collections;

public class LevelComponent : MonoBehaviour {

	public Bounds 	m_bouds;
	public Vector3	m_center
	{
		get{
			return transform.position+m_bouds.center;
		}
		private set{;}
	}
	public bool		m_starter;
	// Use this for initialization
	void Start () {
		DynamicLevelManager.m_singleton.RegisterLevelComponent(this,m_starter);
		//m_bouds = gameObject.GetComponent<Renderer>().bounds;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube( m_center,m_bouds.size);
	}
	void OnDestroy()
	{
		DynamicLevelManager.m_singleton.UnregisterLevelComponent(this);
	}
}
