using UnityEngine;
using System.Collections;

public class SpawningManager : Manager<SpawningManager> {

    int     MAXNPC = 8;
    float   SPAWNCOOLDOWN = 3;
    float m_lastSpawnTimeStamp;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if((Time.time - m_lastSpawnTimeStamp) > SPAWNCOOLDOWN)
        {
            //hellcopter
            if (EntityManager.m_singleton.GetEntityNum(EntityType.Helicopter) < MAXNPC)
            {
                int number = Random.Range(1, 2);
                PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();
                for (int i = 0; i < number; i++)
                {
                    Entity entity = EntityManager.m_singleton.RequestEntity(EntityType.Helicopter, AIServiceManager.m_singleton.GetPredictPlayerPosition(3) + new Vector3(0, i * 2, 0), Quaternion.Euler(0, -90, 0));
                }
                //UnityEditor.EditorApplication.isPaused = true;
            }
            //tank
            if (EntityManager.m_singleton.GetEntityNum(EntityType.Tank) < MAXNPC)
            {
                int number = Random.Range(1, 2);
                PlayerFighter fighter = EntityManager.m_singleton.GetPlayerFighter();
                for (int i = 0; i < number; i++)
                {
                    Entity entity = EntityManager.m_singleton.RequestEntity(EntityType.Tank, AIServiceManager.m_singleton.GetPredictPlayerPosition(3) + new Vector3(0, i * 2, 0), Quaternion.Euler(0, -90, 0));
                }
                //UnityEditor.EditorApplication.isPaused = true;
            }
            m_lastSpawnTimeStamp = Time.time;
        }
        
    }
}
