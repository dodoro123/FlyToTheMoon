using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Vector3 velocity
	{
		get  {return m_velocity;}
	}
	protected Vector3 m_velocity;
    protected float m_velAngle;
    protected float m_acc = 10;
    protected float m_accAngle = 5f;
    protected float m_maxVelChange=10;
	protected float m_MaxVel;
    Vector3 m_targetPoint;
    Vector3 m_lastPos;
	// Use this for initialization
	protected virtual void Start () {
    }
	 protected virtual void UpdateVelocity()
    {
        m_velocity = (transform.position - m_lastPos) / Time.deltaTime;
        m_lastPos = transform.position;
    }
	// Update is called once per frame
	public virtual void Update ()
    {

        if (Vector3.Distance(Vector3.zero, m_targetPoint) > 0.001f)
        {
            //FRCameraController cameraControl = AppManager.m_singleton.GetFRCameraController();
            //m_velocity -= cameraControl.m_velocity;
            m_velAngle += m_accAngle * Time.deltaTime;
            float angle = m_velAngle * Time.deltaTime;
            Vector3 vel = (m_targetPoint - transform.position).normalized* m_MaxVel;


            m_velocity = Vector3.RotateTowards(m_velocity, vel, angle, Mathf.Min(m_MaxVel, m_velocity.magnitude));

            if(m_velocity.magnitude< m_MaxVel)
            {
                m_velocity =m_velocity.normalized* (m_velocity.magnitude+m_acc * Time.deltaTime);
            }

            //m_velocity += cameraControl.m_velocity;
            Debug.DrawRay(transform.position, m_velocity);


            transform.position = transform.position + m_velocity * Time.deltaTime;
            if(Vector3.Distance(transform.position,m_targetPoint)<0.001f)
            {
                m_targetPoint = Vector3.zero;
            }
        }
        //Debug.Log(gameObject.name+"vel:"+ m_velocity.magnitude);
    }

	protected void MoveTo(Vector3 point,float time=1)
	{
        //Debug.DrawLine(transform.position, point);
        transform.position = UnityEngine.Vector3.SmoothDamp(transform.position,point,ref m_velocity, time, m_MaxVel);
        //Debug.Log("m_velocity:"+m_velocity.magnitude);
	}

    protected void BoostTo(Vector3 point)
    {
        m_targetPoint = point;
    }
    protected void Forwarding()
    {
        m_velocity = (transform.position - m_lastPos) / Time.deltaTime;
        transform.position += this.gameObject.transform.forward *m_MaxVel*Time.deltaTime ;
        m_lastPos = transform.position;
    }

    protected Vector3 GetLevelCenter()
    {
        return DynamicLevelManager.m_singleton.GetCurrentCenter();
    }
}
