using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;

public class MainMenuController : MonoBehaviour
{
    SceneController sceneController;

    [SerializeField]
    private UIMainmenuPanel mainmenuPanel;

    [SerializeField]
    private UIAchievementPanel achievementPanel;

    [SerializeField]
    private UICreditPanel creditPanel;

    [SerializeField] private GameObject Handpaper;

    [SerializeField] private GameObject MainMenuPanel;

    bool isInitlize;
    
    private Animator Handhold;
    private CanvasGroup tempMainGroup;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {
       
        sceneController = SharedObject.Instance.Get<SceneController>();
       
    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        mainmenuPanel.Initialize(this);
        achievementPanel.Initialize(this);
        creditPanel.Initialize(this);

        isInitlize = true;
    }

    public void OnLoadSceneGameplay()
    {
        sceneController.OnLoadSceneIntro();
    }

    public void OnClickAchievementButton()
    {
        Debug.Log("==========  Populate Achievement ==========");
        StartCoroutine(HandTransitionoutToAchievement());
        
    }

    public void OnClickCreditButton()
    {
        Debug.Log("==========  Populate  Credit  ==========");
        StartCoroutine(HandTransitionoutToCredit());
    }

    public void OnClickExitButton()
    {
        Debug.Log("==========  Exit Game  ==========");
        Application.Quit();
    }

    public void MainMenuActiveTomain()
    {
        StartCoroutine(HandTransitionInAchievementTomain());
    }
  

    IEnumerator HandTransitionoutToCredit()
    {
        Handpaper.SetActive(true);
        Handhold = Handpaper.GetComponent<Animator>();
        tempMainGroup = MainMenuPanel.GetComponent<CanvasGroup>();
        float t = 1;
        while(t>0f)
        {
            tempMainGroup.alpha = t;
            t -= Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        Handhold.enabled = true;
        Handhold.SetTrigger("Normal");
        yield return new WaitForSeconds(0.35f);
        MainMenuPanel.SetActive(false);
        yield return null;
        creditPanel.Show();
    }
    
    IEnumerator HandTransitionoutToAchievement()
    {
        Handpaper.SetActive(true);
        Handhold = Handpaper.GetComponent<Animator>();
        tempMainGroup = MainMenuPanel.GetComponent<CanvasGroup>();
        float t = 1;
        while(t>0f)
        {
            tempMainGroup.alpha = t;
            t -= Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        Handpaper.SetActive(true);
        Handhold.enabled = true;
        Handhold.SetTrigger("Normal");
        yield return new WaitForSeconds(0.35f);
        MainMenuPanel.SetActive(false);
        achievementPanel.Show();
        yield break;
    }
    IEnumerator HandTransitionInAchievementTomain()
    {
        MainMenuPanel.SetActive(true);
        Handhold = Handpaper.GetComponent<Animator>();
        tempMainGroup = MainMenuPanel.GetComponent<CanvasGroup>();
        Handhold.SetTrigger("Exit");
        yield return new WaitForSeconds(1f);
        tempMainGroup.alpha = 0;
        float t = 0;
        while(t<=1f)
        {
            tempMainGroup.alpha = t;
            t += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        Handpaper.SetActive(false);
       
        yield break;
    }
}

