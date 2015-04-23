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
		MoveTo(pos);
	}
}
