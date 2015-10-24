using UnityEngine;
using System.Collections;

public class Weapon : Entity {

    public bool m_homing;
	Entity m_owner;
    ParticleSystem m_partical;
    Entity m_target;
	// Use this for initialization
	void Start () {
        m_partical = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_homing)
        {
            //select taregt

            m_target = EntityManager.m_singleton.GetRandomEntity(EntityType.Helicopter);


        }
    }

	public void SetupWeapon(Entity owner)
	{
		m_owner = owner;
	}

	public void Fire()
	{
		Quaternion forward = Quaternion.LookRotation(transform.forward,Vector3.up);
        Quaternion random = Quaternion.EulerAngles(Random.Range(-0.05f, 0.05f), 0, 0);
        GameObject bullet = (GameObject)GameObject.Instantiate(Resources.Load("Entities/Bullet"),transform.position+2*transform.forward,forward* random);
		Bullet bulletCom = bullet.GetComponent<Bullet>();
		bulletCom.Init(m_owner);
        if(m_target!=null)
        {
            bulletCom.SetTarget(m_target);
        }
        if(m_partical!=null)
        {
            Debug.DrawRay(transform.position, transform.forward*5, Color.green);
            //m_partical.transform.LookAt(transform.forward, Vector3.up);
            m_partical.Emit(transform.position, transform.forward*500,1,1,Color.black);
        }
        
	}

}
