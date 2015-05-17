using UnityEngine;
using System.Collections;

public class VehicleController : Controller {

	// Use this for initialization
	void Start () {
		m_MaxVel = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
		MoveTo(new Vector3(pos.x,0,pos.z));
		SnapOnGround();
	}


	void SnapOnGround()
	{
		RaycastHit hitInfo;
		if(Physics.Raycast(transform.position,Vector3.down,out hitInfo,1000))
			transform.position = hitInfo.point;	
	}
}
