using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;
using UnityEngine.UI;
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


    void Awake()
    {
        SharedObject.Instance.Add(this);
    }

    private void Start()
    {
        LoadDialogueData();
        //LoadCharacterSprites();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartDialogue();
        }
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

                var data_values = data_string.Split(',');


                if (data_values[0] == "Id" || data_values[0] == "")
                {

                }
                else
                {
                    DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3]);
                    openWith.Add(data_values[0], newDialogue);
                }
            }
        }
    }
    void Initialize()
    {
        CrateNewDialoguePanel();

        dialogueCanvas.enabled = false;

        dialoguePanel.AddListenerToButton(DisplayNextSentence);
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

        dialoguePanel.NameText.text = openWith[currentDialogue].character;
        dialoguePanel.DialogueText.text = openWith[currentDialogue].dialogueText;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentDialogue].dialogueText));

        currentId = openWith[currentDialogue].continueId;
    }

    public void DisplayNextSentence()
    {
        if (currentId == "")
        {
            EndDialogue();
            return;
        }

        dialoguePanel.NameText.text = openWith[currentId].character;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentId].dialogueText));
        currentId = openWith[currentId].continueId;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialoguePanel.DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguePanel.DialogueText.text += letter;
            yield return null;
        }
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
}
