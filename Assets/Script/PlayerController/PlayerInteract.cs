using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public class PlayerInteract : MonoBehaviour
{
    bool isTalk;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TalkWithNPC>()!=null && isTalk == false)
        {
            StartDialogue(other.GetComponent<TalkWithNPC>());
        }
        Destroy(other.gameObject);
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
