using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    bool Dieing= false;
	public EntityType m_type;
	void Awake()
	{
		EntityManager.Singleton.Register(this);
	}

	void OnDie()
	{
        if (!Dieing)
        {
            Dieing = true;
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

            StartCoroutine(RealseEntity());
        }
	}
	IEnumerator RealseEntity()
	{
		yield return new WaitForSeconds(1);
		EntityManager.m_singleton.RealseEntity(m_type,this);
		ParticleSystem vfx = gameObject.GetComponentInChildren<ParticleSystem>();
		if(vfx!=null)
		{
			vfx.Stop();
		}
        Dieing = false;
        yield return 1;
	}

	void OnCollisionEnter(Collision collision)
	{
		Bullet bullet = collision.gameObject.GetComponent<Bullet>();
		if(bullet&&bullet.m_owner is Fighter )
		{
			//do nothing
		}
		else
		{
			Debug.Log(name+" die on collide with "+collision.gameObject.name);
			OnDie ();
		}
	}

	void OnDestroy()
	{
		EntityManager.Singleton.Unregister(this);
	}
}
