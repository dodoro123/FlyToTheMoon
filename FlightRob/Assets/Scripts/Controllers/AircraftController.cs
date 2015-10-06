using UnityEngine;
using System.Collections;

public class AircraftController : Controller {

    // Use this for initialization
    float m_lastPredict;
    Vector3 m_desireOffset;
	void Start () {
        m_maxVelChange = new Vector3(10,10,10);
		m_MaxVel = 60;
        m_desireOffset = new Vector3(Random.Range(-3, 6f), Random.Range(-3, 6f), 0);
    }

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();
        if (Time.time - m_lastPredict > 2)
        {
            m_lastPredict = Time.time;
            m_desireOffset = new Vector3(Random.Range(-3, 6f), Random.Range(-3, 6f), 0);
        }
        //Vector3 pos = AIServiceManager.m_singleton.GetPredictPlayerPosition();
        Vector3 pos = AIServiceManager.m_singleton.GetPlayerPosition();
        pos -= transform.forward*AIServiceManager.m_singleton.GetPlayerVelocity().magnitude*1.5f;
        pos += m_desireOffset;
        MoveTo(pos);
		
	}
    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position,transform.forward);
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position,transform.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position,transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, m_velocity);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
        //Debug.Log("vel:" + m_velocity.magnitude);
    }


}
