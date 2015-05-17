using UnityEngine;
using System.Collections;

public class AppManager : Manager<AppManager>
{

		
	public EntityManager 		m_entityManager {get;private set;}
	public InstantiateManager 	m_instantiateManager {get;private set;}
	public DynamicLevelManager 	m_dynamicLevelManager{get;private set;}
	public AIServiceManager		m_AI{get;private set;}
	public DamageManager		m_DamageManager{get; private set;}

	// Use this for initializationØ
	void Awake () {
		DontDestroyOnLoad(gameObject);
		m_entityManager = gameObject.AddComponent<EntityManager>();
		m_instantiateManager = gameObject.AddComponent<InstantiateManager>();
		m_dynamicLevelManager = gameObject.AddComponent<DynamicLevelManager>();
		m_AI = gameObject.AddComponent<AIServiceManager>();
		m_DamageManager = gameObject.AddComponent<DamageManager>();

	}
	
	// Update is called once per frame
	void Update () {
		//load to level 1 by default
		if(Application.loadedLevel!=1)
			Application.LoadLevel(1);
	}
}
