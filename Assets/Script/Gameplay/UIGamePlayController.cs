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
    private TMP_Text size_Text;

    [SerializeField]
    private TMP_Text size_FullText;

    [SerializeField]
    private GameObject timeContent;

    bool isTimeShow = true;

    bool isPaperShow = false;

    private SceneController sceneController;
    private ClockSystem clockSystem;

    void Awake()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();
    }

    private void Start()
    {
        clockSystem = FindObjectOfType<ClockSystem>();
    }

    public void OnClickTimeButton()
    {
        //Replace this? yes
        // LiftOrPutWatch();
        // isTimeShow = !isTimeShow;
        // timeContent.SetActive(isTimeShow);

        clockSystem.CheckClock();
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
