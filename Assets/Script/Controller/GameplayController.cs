using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;

public class GameplayController : MonoBehaviour
{
    SceneController sceneController;

    DialogueManager dialogueManager;
    
    bool isInitlize;

    private bool isPaused;

    private bool isTimerRuningOut;

    [SerializeField]
    private float MaxTimerSec;

    private float timeRemaining;
    public float TimeRemaining => timeRemaining;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();

        dialogueManager = SharedObject.Instance.Get<DialogueManager>();

        timeRemaining = MaxTimerSec;
    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        DontDestroyOnLoad(this);

        isInitlize = true;
    }

    private void Update()
    {
        UpdateTimer();

        if (!Input.GetKeyDown(KeyCode.Escape)) { return; }

        if (isPaused)
        {
            GameUnpaused();
        }
        else
        {
            GamePaused();
        }
    }

    private void GamePaused()
    {
        Time.timeScale = 0f;
    }

    private void GameUnpaused()
    {
        Time.timeScale = 1f;
    }

    private void UpdateTimer()
    {
        if (isTimerRuningOut) { return; }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            CheckSpecificEvent();
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            isTimerRuningOut = true;
        }
    }

    private void CheckSpecificEvent()
    {

    }

    public void OnLoadSceneMainMenu()
    {
        sceneController.OnLoadSceneMainmenu();

        Discard();
    }

    private void Discard()
    {
        SharedObject.Instance.Remove(this);

        Destroy(this.gameObject);
    }
}
