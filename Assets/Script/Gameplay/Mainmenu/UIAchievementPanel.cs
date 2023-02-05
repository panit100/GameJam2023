using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAchievementPanel : MonoBehaviour
{
    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private GameObject context;

    private MainMenuController mainmenuController;

    [SerializeField] private Toggle FirstEnding;
    [SerializeField] private Toggle SecondEnding;

    public void Initialize(MainMenuController mainmenuController)
    {
        this.mainmenuController = mainmenuController;

        closeButton.onClick.AddListener(OnClickCloseButton);
        if (PlayerPrefs.GetString("FirstEnding") == "True")
        {
            FirstEnding.isOn = true;
        }
        else
        {
            FirstEnding.isOn = false;
        }
        if (PlayerPrefs.GetString("SecondEnding") == "True")
        {
            SecondEnding.isOn = true;
        }
        else
        {
            SecondEnding.isOn = false;
        }
        

        context.SetActive(false);
    }

    public void Show()
    {
        context.SetActive(true);
        
        Populate();

        Debug.Log("Show Achievement Panel animation");
    }

    private void Hide()
    {
        Debug.Log("Hide Achievement Panel animation");

        context.SetActive(false);
    }

    private void OnClickCloseButton()
    {
        Debug.Log("Close Achievement Panel");
        mainmenuController.MainMenuActiveTomain();
        //TODO : Hide panel with animation

        Hide();
    }

    private void Populate()
    {
        Debug.Log("Populate Achievement Panel Complete");
    }
}
