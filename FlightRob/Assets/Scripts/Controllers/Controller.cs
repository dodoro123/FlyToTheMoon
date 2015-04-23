using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {


	public Vector3 velocity
	{
		get  {return m_velocity;}
	}
	protected Vector3 m_velocity;
	protected float m_MaxVel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void MoveTo(Vector3 point)
	{
		transform.position = UnityEngine.Vector3.SmoothDamp(transform.position,point,ref m_velocity,1f,m_MaxVel);
		//Debug.Log("m_velocity:"+m_velocity.magnitude);
	}
}
