using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Utilities;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timer = 0;
    [SerializeField] private bool isInMinute;
    private float minTimer = 0f;

    [Header("Object Setter")]
    [SerializeField] private GameObject clockHand;
    [SerializeField] private GameObject clockCam;

    [Header("For testing only")] [Range(0.1f, 50f)]
    [SerializeField] private float timerSpeedMultiplier = 0;
    
    private InputSystemManager inputSystemManager;
    private bool isToggle;

    private void Start()
    {
        inputSystemManager = SharedObject.Instance.Get<InputSystemManager>();
        inputSystemManager.onCheckClock += CheckClock;
    }

    private void StartTimer()
    {
        if (!isInMinute)
        {
            minTimer = timer / 60;
        }
        else
        {
            minTimer = timer;
        }
        
        //6" per min
        clockHand.transform.rotation = Quaternion.Euler(0f, minTimer * 6f, 0f);
    }

    private void FixedUpdate()
    {
        if (!(timer >= 0))
        {
            timer = 0;
            return;
        }
        
        StartTimer();

        if (isInMinute)
        {
            timer *= 60f;
            isInMinute = false;
        }
        
        timer -= Time.deltaTime * timerSpeedMultiplier;
    }

    private void CheckClock()
    {
        isToggle = !isToggle;

        clockCam.SetActive(isToggle);
    }

    private void OnDestroy()
    {
        inputSystemManager.onCheckClock -= CheckClock;

    }
}
