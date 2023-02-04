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

    public void Initialize(MainMenuController mainmenuController)
    {
        this.mainmenuController = mainmenuController;

        closeButton.onClick.AddListener(OnClickCloseButton);

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

        //TODO : Hide panel with animation

        Hide();
    }

    private void Populate()
    {
        Debug.Log("Populate Achievement Panel Complete");
    }
}
