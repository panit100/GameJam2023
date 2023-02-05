using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using GameJam.Utilities;
using FMOD.Studio;

public class UISoundEvent : MonoBehaviour
{
    SoundManager soundManager;

    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
    }

    public void PlayButtonSFX()
    {
        soundManager.PlayOneShot(soundManager.fModEvent.ButtonSFX,transform.position);
    }
}
