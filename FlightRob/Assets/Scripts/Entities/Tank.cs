using UnityEngine;
using System.Collections;

public class Tank : LiveEntity {

	// Use this for initialization
	void Start () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if ((bullet && bullet.m_owner is PlayerFighter) )
        {
            //Debug.Log(name + " die on collide with " + collision.gameObject.name);
            if (!m_triggerDieing)
                m_triggerDieing = true;
        }
    }
    //override public void Update()
    //{
    //    base.Update();
    //    fire
    //    if (Time.frameCount % 10 == 0)
    //    {
    //        m_weapon.Fire();
    //    }
    //}
}
