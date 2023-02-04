using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public class PlayerInteract : MonoBehaviour
{
    InputSystemManager inputSystemManager;
    bool isTalk;
    void Start()
    {
        inputSystemManager = SharedObject.Instance.Get<InputSystemManager>();
        inputSystemManager.onInteract += OnInteract;
    }
    public void OnInteract(bool value)
    {
        Interacting();
    }

    void Interacting()
    {
        isTalk = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<TalkWithNPC>()!=null && isTalk == true)
        {
            other.GetComponent<TalkWithNPC>().SetDialogueID();
            StartDialogue(other.GetComponent<TalkWithNPC>());
            isTalk = false;
        }
    }

    void StartDialogue(TalkWithNPC NPC)
    {
        //if (PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.CONVERSATION)
        //{
        //    return;
        //}
        if (SharedObject.Instance.Get<DialogueManager>().currentNPC == null)
        {
            SharedObject.Instance.Get<DialogueManager>().currentNPC = NPC;
            SharedObject.Instance.Get<DialogueManager>().currentDialogue = NPC.startWithDialogueId;
        }

        SharedObject.Instance.Get<DialogueManager>().StartDialogue();
        //PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.CONVERSATION;
        return;
    }
}
