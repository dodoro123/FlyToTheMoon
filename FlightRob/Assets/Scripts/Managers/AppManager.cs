using UnityEngine;
using System.Collections;

public class AppManager : Manager<AppManager>
{
    public enum GameState
    {
        InPlanet,
        InUniverse
    };
    GameState m_state;
		
	public EntityManager 		m_entityManager {get;private set;}
	public InstantiateManager 	m_instantiateManager {get;private set;}
	public DynamicLevelManager 	m_dynamicLevelManager{get;private set;}
	public AIServiceManager		m_AI{get;private set;}
	public DamageManager		m_DamageManager{get; private set;}
    public SpawningManager      m_SpawningManager { get; private set; }
    public LootManager          m_LootManager { get; private set; }
    public LevelLoadingManager  m_levelLoadingManager { get; private set; }
    FRCameraController          m_CameraController;

    // Use this for initializationØ
    protected override void Awake () {
        base.Awake();
		DontDestroyOnLoad(gameObject);
		m_entityManager = gameObject.AddComponent<EntityManager>();
		m_instantiateManager = gameObject.AddComponent<InstantiateManager>();
		m_dynamicLevelManager = gameObject.AddComponent<DynamicLevelManager>();
		m_AI = gameObject.AddComponent<AIServiceManager>();
		m_DamageManager = gameObject.AddComponent<DamageManager>();
        m_SpawningManager = gameObject.AddComponent<SpawningManager>();
        m_LootManager = gameObject.AddComponent<LootManager>();
        m_levelLoadingManager = gameObject.AddComponent<LevelLoadingManager>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    void Start()
    {
        //load to level 1 by default
        LevelLoadingManager.m_singleton.LoadLevel(1);
    }
    public void SetFRCameraController(FRCameraController _controller)
    {
        m_CameraController = _controller;
    }
    public FRCameraController GetFRCameraController()
    {
        return m_CameraController;
    }
    public bool IsInPlanet()
    {
        return m_state == GameState.InPlanet;
    }
    public bool IsInUniverse()
    {
        return m_state == GameState.InUniverse;
    }
    public void SetGameState(GameState _state)
    {
        m_state = _state;
    }
}
