using UnityEngine;
using System.Collections;

public class AIServiceManager : Manager<AIServiceManager> {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 GetPlayerVelocity()
	{
		Fighter fighter = (Fighter)EntityManager.m_singleton.GetFighter();
		return fighter.GetVelocity();
	}
	public Vector3 GetPredictPlayerPosition()
	{
		Fighter fighter = (Fighter)EntityManager.m_singleton.GetFighter();

		return fighter.transform.position + 2*fighter.GetVelocity();
	}
}
