using UnityEngine;
using System.Collections;

public class LootsController : Controller {

	// Use this for initialization
	void Start () {
        m_MaxVel = 50;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
        MoveTo(AIServiceManager.m_singleton.GetPredictPlayerPosition(0.1f),0.1f);
	}
}
