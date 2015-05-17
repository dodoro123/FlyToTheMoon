using UnityEngine;
using System.Collections;

public class FRCharactorController : Controller {
	Camera camera;
	FRCharactorBehaviour m_behaviour ;//= new FRCharactorBehaviour();
	Vector3 m_desireVelocity;
	float m_speedMultiplier = 0.3f;
	public float m_enginePower = 5f;
	public float m_gravity = 9.8f;
	public float m_airFraction = 1f;
	public float m_airFraction2 = 5f;
	float m_cruiseSpeed =10f;
	float m_flyPower =0.7f;
	Vector3 m_forwardVel;

	int m_rollDegreeACC = 180;
	int m_rollDegree = 10;
	// Use this for initialization
	void Start ()
	{
		//transform.localPosition = Vector3.zero;
		camera = UnityEngine.Camera.allCameras[0];
		m_behaviour = gameObject.GetComponent<FRCharactorBehaviour>();
		m_velocity = m_cruiseSpeed*transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_behaviour.Update();
		m_desireVelocity = Vector3.zero;
		if(m_behaviour.IsForward())
		{
			m_enginePower=10f;
		}
		if(m_behaviour.IsBackward())
		{
			m_enginePower=2.5f;
		}
		if(m_behaviour.IsUp())
		{
			transform.RotateAround(transform.right,Time.deltaTime);
			//m_desireVelocity += cruiseSpeed*new Vector3(0,1,0);
		}
		else if(m_behaviour.IsDown())
		{
			transform.RotateAround(transform.right,-1*Time.deltaTime);
			//m_desireVelocity += cruiseSpeed*new Vector3(0,-1,0);
		}
		else
		{
			if(transform.up.y<0&&m_rollDegreeACC==180)
			{
				m_rollDegreeACC = 0;
			}

			if(m_rollDegreeACC<180)
			{
				float _rollDegree = Mathf.Lerp(m_rollDegree,0,m_rollDegreeACC/180);
				if((m_rollDegreeACC+_rollDegree)>180)
				{
					_rollDegree = 180 - m_rollDegreeACC;
				}
				m_rollDegreeACC+=(int)_rollDegree;

				transform.Rotate(new Vector3(0,0,_rollDegree));
			}
			else
			{
				m_rollDegreeACC = 180;
				float pitch = transform.forward.y;
				pitch = Mathf.Lerp(pitch,0,0.8f*Time.deltaTime);
				transform.forward = new Vector3(transform.forward.x,pitch,transform.forward.z);
			}

		}
		//Debug.Log(transform.forward+"-"+transform.up);
		m_velocity+=(m_enginePower*transform.forward - m_forwardVel*m_airFraction -(m_velocity-m_forwardVel)*m_airFraction2 +m_cruiseSpeed*m_flyPower*transform.up+m_gravity*Vector3.down)*Time.deltaTime;

		m_cruiseSpeed = Vector3.Dot(m_velocity,transform.forward);
		m_forwardVel = transform.forward*m_cruiseSpeed;
		//if(!OutOfCamera(transform.position+m_desireVelocity*Time.deltaTime))
		{
			transform.position+=m_velocity*Time.deltaTime;
		}

	}

	bool OutOfCamera(Vector3 point)
	{
		Vector3 viewPortPoint = camera.WorldToViewportPoint(point);
		if(viewPortPoint.x>1||viewPortPoint.x<0||viewPortPoint.y>1||viewPortPoint.y<0)
			return true;
		return false;
	}
	public void OnDrawGizmos()
	{
		//Gizmos.color = Color.red;
		//Gizmos.DrawRay(transform.position,transform.forward);
		//Gizmos.color = Color.green;
		//Gizmos.DrawRay(transform.position,transform.up);
		//Gizmos.color = Color.blue;
		//Gizmos.DrawRay(transform.position,transform.right);
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay(transform.position, Vector3.Cross(transform.forward,Vector3.back));
	}
}
