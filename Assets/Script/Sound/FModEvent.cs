using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using GameJam.Utilities;

public class FModEvent : MonoBehaviour
{
    [field : Header("SFX")]
    [field : SerializeField] public EventReference playerWalkSFX {get; private set;}
    [field : SerializeField] public EventReference playerRunSFX {get; private set;}

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
}
