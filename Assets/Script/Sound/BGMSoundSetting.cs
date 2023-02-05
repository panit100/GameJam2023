using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using GameJam.Utilities;
using FMOD.Studio;

public class BGMSoundSetting : MonoBehaviour
{
    [field : Header("BGM")]
    [field : SerializeField] public EventReference BGM {get; private set;}
    [field : SerializeField] public EventReference Ambience {get; private set;}

    SoundManager soundManager;

    EventInstance BGMEvent;
    EventInstance AmbienceEvent;

    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();   

        InitializeBGM();
        InitializeAmbience();
    }

    void InitializeAmbience()
    {
        if(Ambience.IsNull)
            return;

        AmbienceEvent = soundManager.CreateInstance(Ambience);
        
        AmbienceEvent.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            AmbienceEvent.start();
    }

    void InitializeBGM()
    {
        if(BGM.IsNull)
            return;

        BGMEvent = soundManager.CreateInstance(BGM);
        
        BGMEvent.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            BGMEvent.start();
    }

    void OnDestroy()
    {
        AmbienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BGMEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    
}
