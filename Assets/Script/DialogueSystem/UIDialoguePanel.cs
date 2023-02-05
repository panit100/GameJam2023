using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;


public class UIDialoguePanel : MonoBehaviour
{
    [Header("DialogueText")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("Button")]
    [SerializeField] Button continueButton;

    [Header("CutScene")]
    [SerializeField] GameObject imageCutScene;
    Sprite cutSceneSprite;


    public TextMeshProUGUI NameText {get {return nameText;}}
    public TextMeshProUGUI DialogueText {get {return dialogueText;}}
    public Button ContinueButton {get {return continueButton;}}
    public GameObject ImageCutScene { get { return imageCutScene; } }


    public void AddListenerToButton(UnityAction displayNextSentence)
    {

        ContinueButton.onClick.RemoveAllListeners();

        ContinueButton.onClick.AddListener(displayNextSentence);
    }

    public void ResetCharacterSprite()
    {
        imageCutScene.GetComponent<Image>().sprite = null;
        imageCutScene.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
    public void SetCutSceneSprite(Sprite image)
    {
        cutSceneSprite = image;
        imageCutScene.GetComponent<Image>().sprite = cutSceneSprite;
    }

}
