using UnityEngine;
using System.Collections;

public class ProjectileController : Controller
{
    Entity m_target;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        m_MaxVel = 300;
        m_maxVelChange = 10;
	}
	public void SetTarget(Entity _target)
    {
        if(m_target==null&&_target!=null)
        {
            m_target = _target;
            Debug.Log("set target:"+_target.name);
        }
            
    }
	// Update is called once per frame
	public override void Update ()
    {
        UpdateVelocity();
        base.Update();
        if (m_target)
        {
            //heading to target
            BoostTo(m_target.transform.position);
        }
        else
        {
            //heading forward
            Forwarding();
        }
	}
}
