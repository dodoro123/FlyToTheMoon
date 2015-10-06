using UnityEngine;
using System.Collections;

public class Loots : LiveEntity {

	// Use this for initialization
	void Start () {
        m_type = EntityType.Loots;
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " on collide with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<PlayerFighter>())
        {

            if (!m_triggerDieing)
                m_triggerDieing = true;
        }
    }
}
