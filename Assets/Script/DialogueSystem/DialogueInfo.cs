using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInfo
{
    public string ID;
    public string character;
    public string dialogueText;
    public string choice1;

    public DialogueInfo(string Id, string Character, string DialogueText, string Choice1)
    {
        ID = Id;
        character = Character;
        dialogueText = DialogueText;
        choice1 = Choice1;
    }
}