using UnityEngine;
using System.Collections;

public class FRCharactorController : MonoBehaviour {
	Camera camera;
	FRCharactorBehaviour m_behaviour ;//= new FRCharactorBehaviour();
	Vector3 m_desireVelocity;
	float m_speedMultiplier = 0.3f;
	public float m_enginePower = 5f;
	public float m_gravity = 9.8f;
	public float m_airFraction = 0.5f;
	public float m_airFraction2 = 2f;
	float m_cruiseSpeed =5f;
	float m_flyPower =0.5f;
	Vector3 m_velocity;
	Vector3 m_forwardVel;

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
			transform.RotateAround(transform.right,0.1f);
			//m_desireVelocity += cruiseSpeed*new Vector3(0,1,0);
		}
		if(m_behaviour.IsDown())
		{
			transform.RotateAround(transform.right,-0.1f);
			//m_desireVelocity += cruiseSpeed*new Vector3(0,-1,0);
		}

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
}
