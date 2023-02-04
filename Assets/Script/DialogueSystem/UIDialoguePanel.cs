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


    public TextMeshProUGUI NameText {get {return nameText;}}
    public TextMeshProUGUI DialogueText {get {return dialogueText;}}
    public Button ContinueButton {get {return continueButton;}}


    public void AddListenerToButton(UnityAction displayNextSentence)
    {

        ContinueButton.onClick.RemoveAllListeners();

        ContinueButton.onClick.AddListener(displayNextSentence);
    }

}
