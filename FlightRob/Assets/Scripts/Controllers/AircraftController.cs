using UnityEngine;
using System.Collections;

public class AircraftController : Controller {

    // Use this for initialization
    Vector3 m_desireOffset;
	void Start () {
		m_MaxVel = 1000;
        m_desireOffset = new Vector3(Random.Range(-3, 6f), Random.Range(-3, 6f), 0);
    }
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
        pos += m_desireOffset;
        MoveTo(pos);
	}


}
