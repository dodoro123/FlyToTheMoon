using UnityEngine;
using System.Collections;

public class AircraftController : Controller {

	// Use this for initialization
	void Start () {
		m_MaxVel = 100;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
		pos+=new Vector3(Random.Range(-10,10f),Random.Range(-10,10f),0);
		MoveTo(pos);
	}
}
