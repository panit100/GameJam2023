using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject UIDialoguePanel;
    [SerializeField] string[] dialoguePaths;
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();

    [SerializeField] string currentId;
    public TalkWithNPC currentNPC;
    public string currentDialogue;

    Canvas dialogueCanvas;
    UIDialoguePanel dialoguePanel;

    string allSentence;
    [SerializeField] float delayForNextSentences = 5f;

    public Language language;
    public enum Language
    {
        Thai,English
    }
    void Awake()
    {
        SharedObject.Instance.Add(this);
    }

    private void Start()
    {
        LoadDialogueData();
    }
    void LoadDialogueData()
    {
        for (int i = 0; i < dialoguePaths.Length; i++)
        {
            StreamReader stringReader = new StreamReader(Application.streamingAssetsPath + "/DialogueData/" + dialoguePaths[i] + ".csv");

            bool endOfFile = false;
            while (!endOfFile)
            {
                string data_string = stringReader.ReadLine();

                if (data_string == null)
                {
                    endOfFile = true;
                    break;
                }

                var data_values = data_string.Split(';');


                if (data_values[0] == "Id" || data_values[0] == "")
                {

                }
                else
                {
                    DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5]);
                    openWith.Add(data_values[0], newDialogue);
                }
            }
        }
    }
    void Initialize()
    {
        CrateNewDialoguePanel();

        dialogueCanvas.enabled = false;

        //dialoguePanel.AddListenerToButton(DisplayNextSentence);
    }
    void CrateNewDialoguePanel()
    {
        var _dialoguePanel = Instantiate(UIDialoguePanel);
        dialoguePanel = _dialoguePanel.GetComponent<UIDialoguePanel>();
        dialogueCanvas = dialoguePanel.GetComponent<Canvas>();
    }
    public void StartDialogue()
    {
        if (dialoguePanel == null)
            Initialize();

        if (currentDialogue == null)
        {
            return;
        }
        dialogueCanvas.enabled = true;

        if (language == Language.Thai)
        {
            allSentence = openWith[currentDialogue].characterThaiName + " : " + openWith[currentDialogue].dialogueThai;
        }
        else if (language == Language.English)
        {
            allSentence = openWith[currentDialogue].characterEngLishName + " : " + openWith[currentDialogue].dialogueEnglish;
        }

        StopAllCoroutines();
        currentId = openWith[currentDialogue].continueId;
        StartCoroutine(TypeSentence(allSentence));

        //DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentId == "")
        {
            EndDialogue();
            return;
        }

        if (language == Language.Thai)
        {
            allSentence = openWith[currentId].characterThaiName + " : " + openWith[currentId].dialogueThai;
        }
        else if (language == Language.English)
        {
            allSentence = openWith[currentId].characterEngLishName + " : " + openWith[currentId].dialogueEnglish;
        }

        StopAllCoroutines();
        currentId = openWith[currentId].continueId;
        StartCoroutine(TypeSentence(allSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialoguePanel.DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguePanel.DialogueText.text += letter;
            yield return null;
        }
        StartCoroutine(DelayForNextDialogue());
    }

    IEnumerator DelayForNextDialogue()
    {
        yield return new WaitForSeconds(delayForNextSentences);
        DisplayNextSentence();
    }
    void EndDialogue()
    {
        dialogueCanvas.enabled = false;

        if (currentNPC != null)
        {
            currentNPC = null;
        }
        currentDialogue = null;
        //PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }

    public void triggerDialogue(string id)
    {
        currentDialogue = id;
        StartDialogue();
    }


    public void DestroyDialogueManager()
    {
        SharedObject.Instance.Remove(this);
        Destroy(this.gameObject);
    }
}
