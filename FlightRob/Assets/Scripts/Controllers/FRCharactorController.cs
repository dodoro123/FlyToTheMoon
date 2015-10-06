using UnityEngine;
using System.Collections;

public class FRCharactorController : Controller {
	Camera camera;
	FRCharactorBehaviour m_behaviour ;//= new FRCharactorBehaviour();
	Vector3 m_desireVelocity;
	float m_speedMultiplier = 0.3f;
	public float m_enginePower = 3f;
	public float m_gravity = 9.8f;
	public float m_airFraction = 2f;
	public float m_airFraction2 = 10f;
	float m_cruiseSpeed =20f;
	float m_flyPower =0.5f;

    float m_bottomY;

	Vector3 m_forwardVel;

	int m_rollDegreeACC = 180;
	int m_rollDegree = 10;
    // Use this for initialization

    bool    m_lostSpeed;
    float   m_lostSpeedThreshold = 10;
    float   m_maxPullangle = 1;

	void Start ()
	{
		//transform.localPosition = Vector3.zero;
		camera = UnityEngine.Camera.allCameras[0];
		m_behaviour = gameObject.GetComponent<FRCharactorBehaviour>();
		m_velocity = m_cruiseSpeed*transform.forward;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,1000f))
        {
            m_bottomY = transform.position.y - hit.distance;
            Debug.Log("m_bottomY:"+ m_bottomY);
        }
        else
        {
            Debug.LogWarning("no ground found!!!");
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_behaviour.Update();
		m_desireVelocity = Vector3.zero;
        m_enginePower = 5;
        if (m_behaviour.IsForward())
		{
			m_enginePower=10f;
		}
		if(m_behaviour.IsBackward())
		{
			m_enginePower=2.5f;
		}

        float pullAngle = m_maxPullangle;

        if (m_cruiseSpeed < m_lostSpeedThreshold && transform.rotation.eulerAngles.x > 30)
        {
            m_lostSpeed = true;
            Debug.Log("lost speed");
        }
        if(m_cruiseSpeed > 2*m_lostSpeedThreshold&& m_lostSpeed)
        {
            m_lostSpeed = false;
            Debug.Log("get speed");
        }

        if (m_lostSpeed)
        {
            float pitch = transform.forward.y;
            pitch = Mathf.Lerp(pitch, -1000, 0.002f * Time.deltaTime);
            transform.forward = new Vector3(transform.forward.x, pitch, transform.forward.z);
        }
        else
        {
            if (m_behaviour.IsUp() && m_rollDegreeACC == 180)
            {
                transform.RotateAround(transform.right, pullAngle * Time.deltaTime);
                //m_desireVelocity += cruiseSpeed*new Vector3(0,1,0);
            }
            else if (m_behaviour.IsDown() && m_rollDegreeACC == 180)
            {
                transform.RotateAround(transform.right, -1 * pullAngle * Time.deltaTime);
                //m_desireVelocity += cruiseSpeed*new Vector3(0,-1,0);
            }
            else
            {
                if (transform.up.y < 0 && m_rollDegreeACC == 180)
                {
                    m_rollDegreeACC = 0;
                }
                if (m_rollDegreeACC < 180)
                {
                    float _rollDegree = Mathf.Lerp(m_rollDegree, 0, m_rollDegreeACC / 180);
                    if ((m_rollDegreeACC + _rollDegree) > 180)
                    {
                        _rollDegree = 180 - m_rollDegreeACC;
                    }
                    m_rollDegreeACC += (int)_rollDegree;

                    transform.Rotate(new Vector3(0, 0, _rollDegree));
                }
                else
                {
                    m_rollDegreeACC = 180;
                    float pitch = transform.forward.y;
                    pitch = Mathf.Lerp(pitch, 0, 0.8f * Time.deltaTime);
                    transform.forward = new Vector3(transform.forward.x, pitch, transform.forward.z);
                }
            }
        }

        


        float _flyPower = Mathf.Lerp(1, 0.3f, (transform.position.y - m_bottomY)/100) * m_flyPower;

        //Debug.Log("_flyPower" + _flyPower);

        //Debug.Log(transform.forward+"-"+transform.up);
        m_velocity += (m_enginePower*transform.forward - m_forwardVel*m_airFraction -(m_velocity-m_forwardVel)*m_airFraction2 +m_cruiseSpeed*_flyPower*transform.up+m_gravity*Vector3.down)*Time.deltaTime;

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
        //Debug.Log("vel:" + m_velocity.magnitude);
	}
}
