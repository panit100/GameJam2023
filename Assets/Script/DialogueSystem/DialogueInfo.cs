using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInfo
{
    public string ID;
    public string character;
    public string dialogueText;
    public string continueId;

    public DialogueInfo(string Id, string Character, string DialogueText, string ContinueId)
    {
        ID = Id;
        character = Character;
        dialogueText = DialogueText;
        continueId = ContinueId;
    }
}