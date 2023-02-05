using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;
using FMODUnity;
using FMOD.Studio;

public class CutSceneController : MonoBehaviour
{
    SoundManager soundManager;
    SceneController sceneController;

    EventInstance cutScene1;
    EventInstance cutScene2;
    

    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
        sceneController = SharedObject.Instance.Get<SceneController>();
    }

    public void LoadSceneMap1()
    {
        sceneController.OnLoadSceneMap1();
    }

    void PlayCutSceneAmbience1()
    {
        cutScene1 = soundManager.CreateInstance(soundManager.fModEvent.CutScene1);

        cutScene1.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            cutScene1.start();
    }

    void StopCutSceneAmbience1()
    {
        cutScene1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void PlayCutSceneAmbience2()
    {
        cutScene2 = soundManager.CreateInstance(soundManager.fModEvent.CutScene2);

        cutScene2.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            cutScene2.start();
    }

    void StopCutSceneAmbience2()
    {
        cutScene2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    
}
