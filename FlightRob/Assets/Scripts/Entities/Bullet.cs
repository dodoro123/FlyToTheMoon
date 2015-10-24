using UnityEngine;
using System.Collections;

public class Bullet : Entity {
    Entity m_target;
    ProjectileController m_controller;
	public Entity m_owner{get;private set;}
	float startTimeStamp;
	Vector3 initVelocity;
	// Use this for initialization
    void Awake()
    {
        m_controller = GetComponent<ProjectileController>();
    }
	void Start () {
		startTimeStamp  = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        //UnityEditor.EditorApplication.isPaused = true;
        if ((Time.time - startTimeStamp) > 0.1)
        {
            m_controller.SetTarget(m_target);
        }
        if ((Time.time - startTimeStamp)>1)
		{
			Destroy(gameObject);
		}
	}
	public void InitVelocity(Vector3 vel)
	{
		initVelocity = vel;
	}
	public void Init(Entity _entity)
	{
		m_owner = _entity;
	}
	void OnCollisionEnter(Collision collision) 
	{
		//foreach (ContactPoint contact in collision.contacts) 
		//{
		//	Debug.DrawRay(contact.point, contact.normal, Color.white);
		//}
		//if (collision.relativeVelocity.magnitude > 2)
		//	audio.Play();
		
	}
    public void SetTarget(Entity _target)
    {
        m_target = _target;
    }
}
