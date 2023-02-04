using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using GameJam.Utilities;
using FMOD.Studio;

public class UISoundEvent : MonoBehaviour
{
    SoundManager soundManager;
    FModEvent fModEvent;

    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
        fModEvent = SharedObject.Instance.Get<FModEvent>();
    }

    public void PlayButtonSFX()
    {
        soundManager.PlayOneShot(fModEvent.ButtonSFX,transform.position);
    }
}
