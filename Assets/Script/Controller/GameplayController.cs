using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;
using UnityEngine.Events;
using GameJam.GameData;

public class GameplayController : MonoBehaviour
{
    public enum Stage
    {
        Stage1,Stage2,Stage3
    }
    public Stage stage;

    SceneController sceneController;
    private GetAchievement getAch;

    DialogueManager dialogueManager;
    public bool isTriggerDialogue;

    bool isInitlize;

    private bool isPaused;

    private bool isTimerRuningOut;

    [SerializeField]
    private float MaxTimerSec;

    [SerializeField]
    private float timeRemaining;
    public float TimeRemaining => timeRemaining;

    [SerializeField] int collectCard_Stage1;
    [SerializeField] int collectCard_Stage2;
    [SerializeField] int collectCard_Stage3;

    ScaleSetting scaleSetting;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();

        dialogueManager = SharedObject.Instance.Get<DialogueManager>();


        getAch = FindObjectOfType<GetAchievement>();

        scaleSetting = SharedObject.Instance.Get<ScaleSetting>();


        timeRemaining = MaxTimerSec;
        collectCard_Stage1 = 0;
        collectCard_Stage2 = 0;
        collectCard_Stage3 = 0;
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
            getAch.ach = GetAchievement.Achievement.FirstEnding;
            CheckSpecificEvent();
        }
        else
        {
            Debug.Log("Time has run out!");
            getAch.ach = GetAchievement.Achievement.SecondEnding;
            timeRemaining = 0;
            isTimerRuningOut = true;
        }
    }

    
    private void CheckSpecificEvent()
    {
        TimeRemainingDialogue();
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
            if (collectCard_Stage1 == 3)
            {
                var vine = GameObject.Find("Vine1_2");
                vine.GetComponent<Animator>().SetTrigger("VineDown");
                vine.GetComponent<BoxCollider>().enabled = false;

                dialogueManager.triggerDialogue("Ch1_A04_01");
                isTriggerDialogue = true;
            }
        }

        if (stage == Stage.Stage2)
        {
            if (collectCard_Stage2 == 3)
            {
                var vine = GameObject.Find("Vine2_1");
                vine.GetComponent<Animator>().SetTrigger("VineDown");
                vine.GetComponent<BoxCollider>().enabled = false;

                dialogueManager.triggerDialogue("Ch2_A03_01");
                isTriggerDialogue = true;
            }
        }

        if (stage == Stage.Stage3)
        {
            if (collectCard_Stage3 == 3)
            {
                var vine = GameObject.Find("Vine3_2");
                vine.GetComponent<Animator>().SetTrigger("VineDown");
                vine.GetComponent<BoxCollider>().enabled = false;

                var rockGroup = GameObject.Find("RockGroup3_1");
                rockGroup.GetComponent<Animator>().SetBool("RockUp",true);

                dialogueManager.triggerDialogue("Ch3_A03_01");
                isTriggerDialogue = true;
            }
        }
    }

    public void PressButton()
    {
        if (stage == Stage.Stage1)
        {
            var vine = GameObject.Find("Vine1_1");
            vine.GetComponent<Animator>().SetTrigger("VineDown");
            vine.GetComponent<BoxCollider>().enabled = false;
        }

        if (stage == Stage.Stage3)
        {
            var vine = GameObject.Find("Vine3_1");
            vine.GetComponent<Animator>().SetTrigger("VineDown");
            vine.GetComponent<BoxCollider>().enabled = false;

        }
    }

    public void CollectCard()
    {
        if (stage == Stage.Stage1)
        {
            collectCard_Stage1 += 1;
        }
        if (stage == Stage.Stage2)
        {
            collectCard_Stage2 += 1;
        }
        if (stage == Stage.Stage3)
        {
            collectCard_Stage3 += 1;
        }

        CheckCollectCard();

    }

    public void OnLoadSceneMainMenu()
    {
        sceneController.OnLoadSceneMainmenu();

        Discard();
    }

    public void OnLoadSceneMap2()
    {
        sceneController.OnLoadSceneMap2();
        scaleSetting.currentScale = 1;
        StartCoroutine(waitDialogue("Ch2_A01_01"));
        
        stage = Stage.Stage2;
    }

    public void OnLoadSceneMap3()
    {
        sceneController.OnLoadSceneMap3();
        scaleSetting.currentScale = 1;
        StartCoroutine(waitDialogue("Ch3_A01_01"));
        stage = Stage.Stage3;
    }

    IEnumerator waitDialogue( string id)
    {
        yield return new WaitForSeconds(5f);
        dialogueManager.triggerDialogue(id);
        isTriggerDialogue = true;
    }
    private void Discard()
    {
        SharedObject.Instance.Remove(this);
        dialogueManager.DestroyDialogueManager();
        scaleSetting.DestroyScale();

        Destroy(this.gameObject);
    }

    public void TriggerVineAnimation()
    {
        print("TriggerVineAnimation");
    }
    public void SetIsPause(bool newIsPause)
    {
        isPaused = newIsPause;
    }
}
