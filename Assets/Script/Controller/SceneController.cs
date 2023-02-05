using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using GameJam.Utilities;

public class SceneController : MonoBehaviour
{
    public string SCENE_MAINMENU;
    public string SCENE_MAP1;
    public string SCENE_MAP2;
    public string SCENE_MAP3;
    
    Scene loadedSceneBefore;
    bool isInitlize;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {

    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        isInitlize = true;
    }

    public void OnLoadSceneMainmenu()
    {
        OnLoadSceneAsync(SCENE_MAINMENU);
    }

    public void OnLoadSceneMap1()
    {
        OnLoadSceneAsync(SCENE_MAP1);
    }
    
    public void OnLoadSceneMap2()
    {
        OnLoadSceneAsync(SCENE_MAP2);
    }
    
    public void OnLoadSceneMap3()
    {
        OnLoadSceneAsync(SCENE_MAP3);
    }

    public void OnLoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;

        var asyncOparation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!asyncOparation.isDone)
        {
            print("Scene progress : " + asyncOparation.progress);
            yield return null;
        }
        
        asyncOparation.allowSceneActivation = true;
        var loadedScene = SceneManager.GetSceneByName(sceneName);
        
        if(loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
        }

        if(loadedSceneBefore.IsValid())
            SceneManager.UnloadSceneAsync(loadedSceneBefore);

        loadedSceneBefore = loadedScene;
    }
}