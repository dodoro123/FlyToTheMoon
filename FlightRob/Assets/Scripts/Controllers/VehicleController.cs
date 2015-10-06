using UnityEngine;
using System.Collections;

public class VehicleController : Controller
{
    RaycastHit m_groundHitInfo;
    Vector3 initRotation;
    float yawAngle;
    // Use this for initialization
    void Start () {
		m_MaxVel = 30f;
        initRotation = transform.rotation.eulerAngles;
        yawAngle = initRotation.y;
    }
	
	// Update is called once per frame
	public override void Update () {
		Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
		MoveTo(new Vector3(pos.x,0,pos.z));
		SnapOnGround();
	}


	void SnapOnGround()
	{
		if(Physics.Raycast(transform.position,Vector3.down,out m_groundHitInfo, 1000))
        {
            transform.position = m_groundHitInfo.point;
            Quaternion rotation1 = Quaternion.FromToRotation(Vector3.up, m_groundHitInfo.normal);
            Quaternion rotation2 = Quaternion.AngleAxis(yawAngle, Vector3.up);
            //Quaternion rotation2 = Quaternion.LookRotation(m_groundHitInfo.normal,Vector3.right);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation1 * rotation2, 1);
        }
    }
    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position,transform.forward);
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position,transform.up);
        //Gizmos.color = Color.blue;
        ////Gizmos.DrawRay(transform.position,transform.right);
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, m_velocity);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, m_groundHitInfo.normal);
        //Debug.Log("vel:" + m_velocity.magnitude);
    }

}
