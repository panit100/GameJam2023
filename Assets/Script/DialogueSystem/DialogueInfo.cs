using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInfo
{
    public string ID;
    public string characterThaiName;
    public string dialogueThai;
    public string characterEngLishName;
    public string dialogueEnglish;
    public string continueId;
    public string imageCutScene;
    public string type;

    public DialogueInfo(string Id, string CharacterThaiName, string DialogueThai, string CharacterEngLishName, string DialogueEnglish, string ContinueId, string ImageCutScene, string Type)
    {
        ID = Id;
        characterThaiName = CharacterThaiName;
        dialogueThai = DialogueThai;
        characterEngLishName = CharacterEngLishName;
        dialogueEnglish = DialogueEnglish;
        continueId = ContinueId;
        imageCutScene = ImageCutScene;
        type = Type;
    }
}