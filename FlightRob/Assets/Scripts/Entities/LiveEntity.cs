using UnityEngine;
using System.Collections;

public class LiveEntity : Entity {
    protected bool m_triggerDieing = false;
    bool m_dieing = false;
    // Use this for initialization
    void Start () {
	
	}
	protected virtual void OnDead()
    {
        LootManager.m_singleton.RequestLoots(m_type, transform.position);
    }
	// Update is called once per frame
	virtual public void Update ()
    {
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
            OnDead();
            StartCoroutine(RealseEntity());
        }
        if(Vector3.Distance(EntityManager.m_singleton.GetPlayerFighter().transform.position,transform.position)>500)
        {
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
    public override void OnAwake()
    {
        base.OnAwake();
        m_triggerDieing = false;
        m_dieing = false;
    }
}
