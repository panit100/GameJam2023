using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkWithNPC : MonoBehaviour
{
    public string startWithDialogueId;

    public void ChangeDialogueId(string changeDialogueId)
    {
        startWithDialogueId = changeDialogueId;
    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

}
