using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;
using UnityEngine.Events;

public class GameplayController : MonoBehaviour
{
    public enum Stage
    {
        Stage1,Stage2,Stage3
    }
    public Stage stage;

    SceneController sceneController;

    DialogueManager dialogueManager;
    bool isTriggerDialogue;

    bool isInitlize;

    private bool isPaused;

    private bool isTimerRuningOut;

    [SerializeField]
    private float MaxTimerSec;

    [SerializeField]
    private float timeRemaining;
    public float TimeRemaining => timeRemaining;

    int collectCard;

    public List<UnityEvent> unityEvents = new List<UnityEvent>(); 
    public UnityEvent unityEvent;

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
        TimeRemainingDialogue();
        CheckCollectCard();
        unityEvent.Invoke();
    }

    private void TimeRemainingDialogue()
    {
        if(stage == Stage.Stage1)
        {
            if (timeRemaining <= 600 && timeRemaining >= 595 && isTriggerDialogue == false)
            {
                dialogueManager.triggerDialogue("Ch1_A01_01");
                isTriggerDialogue = true;
            }
            if (timeRemaining <= 480 && timeRemaining >= 475 && isTriggerDialogue == false)
            {
                dialogueManager.triggerDialogue("Ch1_A02_01");
                isTriggerDialogue = true;
            }
            else if (timeRemaining <= 360 && timeRemaining >= 355 && isTriggerDialogue == false)
            {
                dialogueManager.triggerDialogue("Ch1_A03_01");
                isTriggerDialogue = true;
            }
        }
    }

    private void CheckCollectCard()
    {
        if (stage == Stage.Stage1)
        {
            if (collectCard == 2)
            {
                // trigger vine
            }
        }
    }

    public void CollectCard()
    {
        collectCard += 1;
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

    public void TriggerVineAnimation()
    {
        print("TriggerVineAnimation");
    }
}
