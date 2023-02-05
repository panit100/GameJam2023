using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameJam.Utilities;

public class UIGamePlayController : MonoBehaviour
{
    [SerializeField]
    private Animator leftHandAnimator;

    [SerializeField]
    private Animator rightHandAnimator;

    [SerializeField]
    private Animator tutorialAnimator;

    [SerializeField]
    private Animator exitPanelAnimator;

    [SerializeField]
    private Animator gamePlayPanelAnimator;

    [SerializeField]
    private TMP_Text size_Text;

    [SerializeField]
    private TMP_Text size_FullText;

    [SerializeField]
    private GameObject timeContent;

    bool isPause = true;


    private SceneController sceneController;
    private GameplayController gameplayController;
    private ClockSystem clockSystem;

    void Awake()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();
        gameplayController = SharedObject.Instance.Get<GameplayController>();
    }

    private void Start()
    {
        clockSystem = FindObjectOfType<ClockSystem>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) { return; }

        OnClickExitButton();

        isPause = !isPause;

        gameplayController.SetIsPause(isPause);
    }

    public void OnClickTimeButton()
    {
        //Replace this? yes
        // LiftOrPutWatch();
        // isTimeShow = !isTimeShow;
        // timeContent.SetActive(isTimeShow);

        clockSystem.CheckClock();
        gamePlayPanelAnimator.SetTrigger("trigger");
    }

    public void OnClickTutorialButton()
    {
        LiftOrPutPaper();
        tutorialAnimator.SetTrigger("Lift");
    }

    public void OnClickExitButton()
    {
        exitPanelAnimator.SetTrigger("trigger");
    }

    public void OnClickResumeButton()
    {
        exitPanelAnimator.SetTrigger("trigger");
    }

    public void OnClickMenuExitButton()
    {
        sceneController.OnLoadSceneMainmenu();
    }

    public void SetSizeText(string sizeText,string sizeFullText)
    {
        size_Text.text = sizeText;
        size_FullText.text = sizeFullText;
    }

    public void LiftOrPutWatch()
    {
        leftHandAnimator.SetTrigger("Lift");
    }

    public void LiftOrPutPaper()
    {
        rightHandAnimator.SetTrigger("Lift");
    }
}
