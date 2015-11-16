using UnityEngine;
using System.Collections;

public class LevelLoadingManager : Manager<LevelLoadingManager> 
{

    //IEnumerator Start()
    //{
        //loading universe level
        //AsyncOperation async = Application.LoadLevelAdditiveAsync(2);
        //yield return async;
        //Debug.Log("Loading universe complete");
    //}

    // Update is called once per frame
    void Update () {

	}

    public void LoadLevel(int _levelIndex)
    {
        if (Application.loadedLevel != _levelIndex)
        {
            Application.LoadLevel(_levelIndex);
            //todo: no depend on level index
            if(_levelIndex==1)
                AppManager.m_singleton.SetGameState(AppManager.GameState.InPlanet);
            if(_levelIndex==2)
                AppManager.m_singleton.SetGameState(AppManager.GameState.InUniverse);
        }

    }
    //public bool islevelLoaded(int _levelIndex)
    //{
    //    Application.isLoadingLevel();
    //}
}
