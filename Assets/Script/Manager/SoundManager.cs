using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using GameJam.Utilities;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    bool isInitlize;

    void Awake()
    {
        Initilize();
    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        isInitlize = true;
    }

    public void PlayOneShot(EventReference sound,Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound,position);
    }

    public EventInstance CreateInstance(EventReference reference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(reference);
        
        return eventInstance;
    }

    public void AttachInstanceToGameObject(EventInstance eventInstance,Transform transform,Rigidbody rigidbody)
    {
        RuntimeManager.AttachInstanceToGameObject(eventInstance,transform,rigidbody);
    }
}
