using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {


	public Vector3 velocity
	{
		get  {return m_velocity;}
	}
	protected Vector3 m_velocity;
    protected Vector3 m_maxVelChange;
	protected float m_MaxVel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
        //Debug.Log(gameObject.name+"vel:"+ m_velocity.magnitude);
	}

	protected void MoveTo(Vector3 point,float time=1)
	{
        Debug.DrawLine(transform.position, point);
        transform.position = UnityEngine.Vector3.SmoothDamp(transform.position,point,ref m_velocity, time, m_MaxVel);
		//Debug.Log("m_velocity:"+m_velocity.magnitude);
	}
    protected Vector3 GetLevelCenter()
    {
        return DynamicLevelManager.m_singleton.GetCurrentCenter();
    }
}
