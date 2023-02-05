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
        Handhold = Handpaper.GetComponent<Animator>();
        tempMainGroup = MainMenuPanel.GetComponent<CanvasGroup>();
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
        sceneController.OnLoadSceneMap1();
    }

    public void OnClickAchievementButton()
    {
        Debug.Log("==========  Populate Achievement ==========");
       
      
        StartCoroutine(HandTransitionout());
        
    }

    public void OnClickCreditButton()
    {
        Debug.Log("==========  Populate  Credit  ==========");

        creditPanel.Show();
    }

    public void OnClickExitButton()
    {
        Debug.Log("==========  Exit Game  ==========");
    }

    public void MainMenuActive()
    {
        StartCoroutine(HandTransitionIn());
    }

   
    IEnumerator HandTransitionout()
    {
        while(tempMainGroup.alpha>0f)
        {
            tempMainGroup.alpha -= Time.fixedDeltaTime;
            yield return null;
        }
        Handpaper.SetActive(true);
        Handhold.enabled = true;
        Handhold.SetTrigger("Normal");
        Debug.Log("oooof");
        yield return new WaitForSeconds(0.35f);
        MainMenuPanel.SetActive(false);
       
        achievementPanel.Show();
        yield break;
    }

    IEnumerator HandTransitionIn()
    {
        Handhold.SetTrigger("Exit");
        yield return new WaitForSeconds(1f);
        Handpaper.SetActive(false);
        MainMenuPanel.SetActive(true);
        tempMainGroup.alpha = 0;
        float temp = tempMainGroup.alpha;
        while (temp<=1f)
        {
            tempMainGroup.alpha += Time.fixedDeltaTime;
            yield return null;
            temp = tempMainGroup.alpha;
        }
        yield break;
    }
}

