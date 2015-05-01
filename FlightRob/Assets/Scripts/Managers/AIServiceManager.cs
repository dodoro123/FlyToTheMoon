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
		PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();
		return fighter.GetVelocity();
	}
	public Vector3 GetPredictPlayerPosition()
	{
		PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();

		return fighter.transform.position + 1.5f*fighter.GetVelocity();
	}
}
