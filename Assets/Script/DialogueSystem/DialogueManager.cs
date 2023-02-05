using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject UIDialoguePanel;
    [SerializeField] string[] dialoguePaths;
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();

    [SerializeField] List<Sprite> characterSprites = new List<Sprite>();
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
    public enum Type
    {
        Dialogue,
        CutScene
    }
    [Header("DialogueType")]
    public Type type;
    void LoadCutSceneSprites()
    {
        characterSprites = Resources.LoadAll<Sprite>("DialogueSprite").ToList();
    }

    void Awake()
    {
        SharedObject.Instance.Add(this);
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        LoadDialogueData();
        LoadCutSceneSprites();
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
                    DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5], data_values[6], data_values[7]);
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
        var _dialoguePanel = Instantiate(UIDialoguePanel,this.transform);
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
        dialoguePanel.ResetCharacterSprite();
        type = (Type)Enum.Parse(typeof(Type), openWith[currentDialogue].type);
        dialogueCanvas.enabled = true;
        CheckDialogueType(currentDialogue);

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

    }
    void CheckDialogueType(string checkChoiceId)
    {
        if (type == Type.CutScene)
        {
            var cutSceneImage = characterSprites.Find(n => n.name == openWith[checkChoiceId].imageCutScene);
            dialoguePanel.SetCutSceneSprite(cutSceneImage);
            dialoguePanel.ImageCutScene.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void DisplayNextSentence()
    {
        if (currentId == "")
        {
            EndDialogue();
            return;
        }
        CheckDialogueType(currentId);

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

        SharedObject.Instance.Get<GameplayController>().isTriggerDialogue = false;
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
