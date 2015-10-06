using UnityEngine;
using System.Collections;

public class LootManager : Manager<LootManager>
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void RequestLoots(EntityType _type,Vector3 position)
    {
        float loot = Random.Range(0.0f, 1.0f);
        if(loot >0.5f)
        {
            EntityManager.m_singleton.RequestEntity(EntityType.Loots, position, Quaternion.identity);
        }
        
    }
}
