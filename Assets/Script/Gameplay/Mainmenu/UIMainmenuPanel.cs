using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button achievementButton;

    [SerializeField]
    private Button creditButton;

    [SerializeField]
    private Button exitButton;

    private MainMenuController mainmenuController;

    public void Initialize(MainMenuController mainmenuController)
    {
        this.mainmenuController = mainmenuController;

        Subscribe();
    }

    void Subscribe()
    {
        playButton.onClick.AddListener(mainmenuController.OnLoadSceneGameplay);

        achievementButton.onClick.AddListener(mainmenuController.OnClickAchievementButton);

        creditButton.onClick.AddListener(mainmenuController.OnClickCreditButton);

        exitButton.onClick.AddListener(mainmenuController.OnClickExitButton);
    }
}
