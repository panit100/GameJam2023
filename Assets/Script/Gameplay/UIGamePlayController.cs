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

    void Awake()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();
    }

    public void OnClickTimeButton()
    {
        LiftOrPutWatch();
        isTimeShow = !isTimeShow;
        timeContent.SetActive(isTimeShow);
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
