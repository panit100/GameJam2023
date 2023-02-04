using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreditPanel : MonoBehaviour
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

        Debug.Log("Show Credit Panel animation");
    }

    private void Hide()
    {
        Debug.Log("Hide Credit  Panel animation");

        context.SetActive(false);
    }

    private void OnClickCloseButton()
    {
        Debug.Log("Close Credit  Panel");

        //TODO : Hide panel with animation

        Hide();
    }

    private void Populate()
    {
        Debug.Log("Populate Credit Panel Complete");
    }
}
