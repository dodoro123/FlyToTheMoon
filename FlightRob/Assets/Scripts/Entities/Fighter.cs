using UnityEngine;
using System.Collections;

public class Fighter : Entity {

	WeaponPlatform 			m_platform;
	Weapon 					m_weapon;
	Controller	m_controller;
	// Use this for initialization
	void Start () 
	{
		m_platform = gameObject.AddComponent<WeaponPlatform>();
		m_controller = (Controller)gameObject.GetComponent(typeof(Controller));
		
		
		m_platform.PlacePlatform(new Vector3(0,-0.5f,0),transform.localRotation);
		Object gun = Resources.Load("Entities/Gun");
		GameObject weapon = (GameObject)GameObject.Instantiate(gun,Vector3.zero,Quaternion.identity);
		m_weapon = weapon.GetComponent<Weapon>();
		m_platform.AttachWeaponToSlot(m_weapon);
		Quaternion forward = Quaternion.LookRotation(transform.forward,Vector3.up);
		m_weapon.transform.rotation=forward;
		m_weapon.SetupWeapon(this);

        m_triggerDieing = false;
        m_dieing = false;


    }

	public Vector3 GetVelocity()
	{
		return m_controller.velocity;
	}

    bool m_triggerDieing = false;
    bool m_dieing = false;
    void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if ((bullet && bullet.m_owner is Fighter) || collision.gameObject.GetComponent<Fighter>())
        {
            //do nothing
        }
        else
        {
            //Debug.Log(name+" die on collide with "+collision.gameObject.name);
            if (!m_triggerDieing)
                m_triggerDieing = true;
        }
    }
    IEnumerator RealseEntity()
    {
        yield return new WaitForSeconds(1);
        EntityManager.m_singleton.RealseEntity(m_type, this);
        ParticleSystem vfx = gameObject.GetComponentInChildren<ParticleSystem>();
        if (vfx != null)
        {
            vfx.Stop();
        }
        m_dieing = false;
    }
    void Update()
    {
        //fire
        if (Time.frameCount % 10 == 0)
        {
            m_weapon.Fire();
        }
        //die
        if (m_triggerDieing && !m_dieing)
        {
            m_dieing = true;
            Renderer[] renders = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var render in renders)
            {
                render.enabled = false;
            }
            ParticleSystem vfx = gameObject.GetComponentInChildren<ParticleSystem>();
            if (vfx != null)
            {
                vfx.Play();
            }
            Debug.Log("OnDie:" + name + " t:" + Time.frameCount);
            StartCoroutine(RealseEntity());
        }
    }
    public override void OnAwake()
    {
        base.OnAwake();
        m_triggerDieing = false;
        m_dieing = false;
    }
}
