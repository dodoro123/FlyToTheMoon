using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

	Vector3 m_velocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
		MoveTo(new Vector3(pos.x,0,pos.z));
		SnapOnGround();
	}

	void MoveTo(Vector3 point)
	{
		transform.position = UnityEngine.Vector3.SmoothDamp(transform.position,point,ref m_velocity,1f);
	}

	void SnapOnGround()
	{
		RaycastHit hitInfo;
		if(Physics.Raycast(transform.position,Vector3.down,out hitInfo,1))
			transform.position = hitInfo.point;	
	}
}
