using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Utilities;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    private float minTimer = 0f;

    [Header("Object Setter")]
    [SerializeField] private GameObject clockHand;
    [SerializeField] private GameObject clockCam;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject playerHand;

    [Header("For testing only")] [Range(0.1f, 50f)]
    [SerializeField] private float timerSpeedMultiplier = 0;
    
    private InputSystemManager inputSystemManager;
    private GameplayController gameplayController;
    private PlayerAnimationController playerAnimationController;
    private PlayerMovementController playerMovementController;
    private bool isToggle;

    private void Start()
    {
        inputSystemManager = SharedObject.Instance.Get<InputSystemManager>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerMovementController = GetComponent<PlayerMovementController>();
        inputSystemManager.onCheckClock += CheckClock;

        gameplayController = FindObjectOfType<GameplayController>();
    }

    private void StartTimer()
    {
        minTimer = gameplayController.TimeRemaining / 60;
        
        //6" per min
        clockHand.transform.localRotation = Quaternion.Euler(0f, 0f, minTimer * 6f);
    }

    private void LateUpdate()
    {
        StartTimer();
    }

    private void CheckClock()
    {
        isToggle = !isToggle;

        if (isToggle)
        {
            playerAnimationController.UseClockAnimation();
            Invoke(nameof(DisablePlayer), 2f);
        }
        else
        {
            playerAnimationController.UnUseClockAnimation();
            EnablePlayer();
        }

        clockCam.SetActive(isToggle);
        
    }

    private void DisablePlayer()
    {
        playerHand.SetActive(true);
        playerModel.SetActive(false);
        playerMovementController.canMove = false;
        playerMovementController.canWalk = false;
        playerMovementController.canJump = false;
    }
    
    private void EnablePlayer()
    {
        playerHand.SetActive(false);
        playerModel.SetActive(true);
        playerMovementController.canMove = true;
        playerMovementController.canWalk = true;
        playerMovementController.canJump = true;
    }
    
    private void OnDestroy()
    {
        inputSystemManager.onCheckClock -= CheckClock;
    }
}
