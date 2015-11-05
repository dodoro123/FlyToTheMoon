using UnityEngine;
using System.Collections;

public class FRCameraController : MonoBehaviour
{
	public Vector3 m_velocity;
	PlayerFighter m_target;
	// Use this for initialization
	void Start () {
		m_target = EntityManager.m_singleton.GetPlayerFighter();
        AppManager.m_singleton.SetFRCameraController(this);
    }
	
	// Update is called once per frame
	void LateUpdate () {
		//transform.position+=m_moveDir;
		Follow(m_target.transform.position+new Vector3(0,0,-45)+1.1f*m_target.GetVelocity());
	}
	void Follow(Vector3 pos)
	{
		transform.position = UnityEngine.Vector3.SmoothDamp(transform.position,pos,ref m_velocity,0.8f);
	}

}
