using System.Collections;
using System.Collections.Generic;
using System.IO;
using DialogueMaker.Classes;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the main window of the program 'Dialogue Maker'.
/// </summary>
public class DialogueMaker_MainWindow : MonoBehaviour
{
    [Header("Inputs")]
    public GameObject filenameInput;
    public GameObject npcInput;
    public GameObject dialogueInput;
    public GameObject answerNumberInput;
    public GameObject answerInput;
    public GameObject versionInput;
    public GameObject emotionInput;

    [Header("Section texts")]
    public Text ncpInputText;
    public Text dialogueInputText;
    public Text answerNumberInputText;
    public Text answerInputText;
    public Text emotionInputText;

    [Header("Others")]
    public GameObject optionButton;

    public DialogueMakerScript dialogueMakerScript;
    public Text checkText;

    public string pathToStore;

    private string type = "Dialogue";
    private string filename;
    private string speaker;
    private string line;
    private string emotion;
    private int number;
    private int version;

    //Initialize the program and all the values.
    void Start()
    {
        filenameInput.GetComponentInChildren<InputField>().text = "";
		dialogueInput.GetComponentInChildren<InputField>().text = "";
		answerNumberInput.GetComponentInChildren<InputField>().text = "";
		answerInput.GetComponentInChildren<InputField>().text = "";
        versionInput.GetComponentInChildren<InputField>().text = "";
        emotionInput.GetComponentInChildren<InputField>().text = "";
        answerInput.SetActive(false);
        answerInputText.enabled = false;
    }

    /// <summary>
    /// If there is a filename and the Dialogue boxes are on-screen, we'll submitDialogue; if the Question boxes are on screen,
    /// we'll submitQuestion.
    /// </summary>
    public void submit()
    {
        filename = filenameInput.GetComponentInChildren<InputField>().text;
        string v = versionInput.GetComponentInChildren<InputField>().text;
        if (pathToStore == "" || pathToStore.Length <=1) pathToStore = Application.dataPath;
        if (filename == "") Debug.Log("There's no filename.");
        else
        {
            switch (type)
            {
                case "Dialogue":
                    submitDialogue();
                    break;
                case "Question":
                    submitQuestion();
                    break;
            }
            dialogueInput.GetComponentInChildren<InputField>().text = "";
		    answerNumberInput.GetComponentInChildren<InputField>().text = "";
		    answerInput.GetComponentInChildren<InputField>().text = "";
        }
    }

    /// <summary>
    /// Grab all the information needed to store dialogue. If the file already exists, call SaveDialogueInExistingFile;
    /// otherwise, call SaveDialogueInNewFile.
    /// </summary>
    private void submitDialogue()
    {
        Debug.Log("Saving dialogue.");
        //filename = filenameInput.GetComponentInChildren<Text>().text;
        speaker = npcInput.GetComponentInChildren<InputField>().text;
        line = dialogueInput.GetComponentInChildren<InputField>().text;
        emotion = emotionInput.GetComponentInChildren<InputField>().text;
        string n = answerNumberInput.GetComponentInChildren<InputField>().text;
        string v = versionInput.GetComponentInChildren<InputField>().text;
        DialogueLine d = new DialogueLine();
        d.line = line;
        d.name = speaker;
        d.emotion = emotion;
        string p =  pathToStore + "/" + filename + ".json";
        if (FileExists(filename))
        {
            dialogueMakerScript.SaveDialogueInExistingFile(p, n, v, d);
        }
        else dialogueMakerScript.SaveDialogueInNewFile(p, d);
    }

    /// <summary>
    /// Grab all the information needed to store question. If the file already exists, call SaveQuestionInExistingFile;
    /// otherwise, call SaveQuestionInNewFile.
    /// </summary>
    private void submitQuestion()
    {
        Debug.Log("Saving question.");
        line = answerInput.GetComponentInChildren<InputField>().text;
        string n = answerNumberInput.GetComponentInChildren<InputField>().text;
        string v = versionInput.GetComponentInChildren<InputField>().text;
        Answer a = new Answer();
        a.line = line;
        string p =  pathToStore + "/" + filename + ".json";
        if (FileExists(filename))
        {
            dialogueMakerScript.SaveQuestionInExistingFile(p, n, v, a);
        }
        else dialogueMakerScript.SaveQuestionInNewFile(p, a);
    }

    /// <summary>
    /// If the file in path 'f' exists, returns true; otherwise, returns false.
    /// </summary>
    public bool FileExists(string f)
    {
        string path = pathToStore + "/" + f + ".json";
		if (!File.Exists(path)) return false;
        else return true;
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }

    /// <summary>
    /// If we're in Dialogue mode, we switch to Question; if we're in Question mode, we switch to Dialogue.
    /// </summary>
    public void ChangeType()
    {
        type = optionButton.GetComponentInChildren<Text>().text;
        if (type == "Dialogue")
        {
            //Acexivar "Question"
            ncpInputText.enabled = false;
            npcInput.SetActive(false);
            dialogueInputText.enabled = false;
            dialogueInput.SetActive(false);
            emotionInputText.enabled = false;
            emotionInput.SetActive(false);
            answerInput.SetActive(true);
            answerInputText.enabled = true;
            optionButton.GetComponentInChildren<Text>().text = "Question";
            type = "Question";
        }
        else
        {
            ncpInputText.enabled = true;
            npcInput.SetActive(true);
            dialogueInputText.enabled = true;
            dialogueInput.SetActive(true);
            emotionInputText.enabled = true;
            emotionInput.SetActive(true);
            answerInput.SetActive(false);
            answerInputText.enabled = false;
            optionButton.GetComponentInChildren<Text>().text = "Dialogue";
            type = "Dialogue";
        }
    }

    /// <summary>
    /// If the file exists, display the information on screen.
    /// </summary>
    public void CheckFile()
    {
        string f = filenameInput.GetComponentInChildren<InputField>().text;
        if (FileExists(f))
        {
            string information = File.ReadAllText(pathToStore + "/" + f + ".json");
            checkText.text = information;
        }
        StartCoroutine(AdjustCheckWindow());
    }

    /// <summary>
    /// Make the scroll rect go to the top when loaded.
    /// </summary>
    private IEnumerator AdjustCheckWindow()
    {
        yield return new WaitForEndOfFrame();
        Scrollbar sb = checkText.gameObject.GetComponentInParent<ScrollRect>().verticalScrollbar;
        sb.value = 1.0f;
    }
}
