using UnityEngine;
using System.Collections;

public class FRCameraController : MonoBehaviour
{
	public Vector3 m_moveDir;
	Fighter m_target;
	// Use this for initialization
	void Start () {
		m_target = (Fighter)EntityManager.m_singleton.GetFighter();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position+=m_moveDir;
		Follow(m_target.transform.position+new Vector3(0,5,-25)+m_target.GetVelocity());
	}
	void Follow(Vector3 pos)
	{
		transform.position = Vector3.Lerp(transform.position,pos,1f);
	}
}
