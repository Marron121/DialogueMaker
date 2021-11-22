using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using SFB;

/// <summary>
/// This script controls all the functions in the Options window.
/// </summary>
public class DialogueMakerOptions : MonoBehaviour
{
    public DialogueMaker_MainWindow dialogueMaker;
    public GameObject optionsWindow;
    public GameObject creditsWindow;

    public GameObject checkWindow;
    public Text pathTextButton;
    public List<Text> texts;
    public List<Text> allTexts;
    public Font dyslexicFont;

    public List<RawImage> backgroundImages;
    bool isOptionsWindowOpen = false;
    bool isCreditsWindowOpen = false;
    bool isCheckWindowOpen = false;
    bool isDyslexicFontActive = false;

    public List<GameObject> presetButtons;

    /// <summary>
    /// If there's a save option in the application folder, load it.
    /// </summary>
    private void Awake()
    {
      if (File.Exists(Application.dataPath+"/DialogueMaker_Options.txt"))
        ReadOptionsFromFile(Application.dataPath+"/DialogueMaker_Options.txt"); 
    }

    /// <summary>
    /// If the Options window is closed, we open it; otherwise, we close it.
    /// </summary>
    public void OpenAndCloseOptionsWindow()
    {
        isOptionsWindowOpen = !isOptionsWindowOpen;
        optionsWindow.SetActive(isOptionsWindowOpen);
    }

    /// <summary>
    /// If the Credits window is closed, we open it; otherwise, we close it.
    /// </summary>
    public void OpenAndCloseCreditsWindow()
    {
        isCreditsWindowOpen = !isCreditsWindowOpen;
        creditsWindow.SetActive(isCreditsWindowOpen);
    }

    /// <summary>
    /// If the Check window is closed, we open it; otherwise, we close it.
    /// </summary>
    public void OpenAndCloseCheckWindow()
    {
        isCheckWindowOpen = !isCheckWindowOpen;
        checkWindow.SetActive(isCheckWindowOpen);
    }

    /// <summary>
    /// Saves the path were the dialogue files will be saved.
    /// </summary>
    public void GetStorePath()
    {
        ChangeStorePath(StandaloneFileBrowser.OpenFolderPanel("Select folder to store dialogue", "", false)[0]);
    }

    /// <summary>
    /// Stores the path in the 'dialogue_maker' script.
    /// </summary>
    private void ChangeStorePath(string path)
    {
        dialogueMaker.pathToStore = path;
        pathTextButton.text = dialogueMaker.pathToStore;
    }

    /// <summary>
    /// Resets the path to store the json files.
    /// </summary>
    public void ResetPath()
    {
        dialogueMaker.pathToStore = "";
        pathTextButton.text = "";
    }

    /// <summary>
    /// If 'state' is true, we change all the text fonts to use a dyslexic friendly one; otherwise, we use arial.
    /// </summary>
    public void ChangeIfDyslexicFont(bool state)
    {
        isDyslexicFontActive = state;
        Font font = null;
        if (isDyslexicFontActive) font = dyslexicFont;
        else font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        foreach (Text t in allTexts) t.font = font;
    }

    /// <summary>
    /// The user chooses which color to use for the backgrounds.
    /// </summary>
    public void ChangeBackgroundColor()
    {
        ColorPicker.Create(backgroundImages[0].color, "Select the background's color", SetColorBackground, ColorFinished, true);
        foreach (GameObject b in presetButtons) b.SetActive(false);
    }

    /// <summary>
    /// The user chooses which color to use for the text (excluding buttons).
    /// </summary>
    public void ChangeTextColor()
    {
        ColorPicker.Create(texts[0].color, "Select the text's color", SetColorText, ColorFinished, true);
        foreach (GameObject b in presetButtons) b.SetActive(false);
    }

    /// <summary>
    /// Set the color of backgrounds in 'backgroundImages' to 'currentColor'.
    /// </summary>
    private void SetColorBackground(Color currentColor)
    {
        foreach (RawImage bg in backgroundImages) bg.color = currentColor;
        Camera.main.backgroundColor = currentColor;
    }

    /// <summary>
    /// Set the color of texts in 'texts' to 'currentColor'.
    /// </summary>
    private void SetColorText(Color currentColor)
    {
        foreach (Text t in texts) t.color = currentColor;
    }


    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
        foreach (GameObject b in presetButtons) b.SetActive(true);
    }

    /// <summary>
    /// Create .txt file with the data from Options and stores it in the place the user chooses.
    /// </summary>
    public void SaveOptions()
    {
        string options = "0?";
        if (isDyslexicFontActive) options = "1?";
        options += pathTextButton.text + "?" + ColorUtility.ToHtmlStringRGBA(backgroundImages[0].color) + "?" +
        ColorUtility.ToHtmlStringRGBA(texts[0].color);
        Debug.Log(options);
        string [] pathFile;
        pathFile = StandaloneFileBrowser.OpenFolderPanel("Select folder to save options file", "", false);
        if (pathFile.Length is 0) return;
        File.WriteAllText(pathFile[0] + "/" + "DialogueMaker_Options.txt", options);
    }

    /// <summary>
    /// Loads .txt file with the data from Options (if file exists in the folder choosen by user).
    /// </summary>
    public void LoadOptions()
    {
        string [] pathFile;
        pathFile = StandaloneFileBrowser.OpenFilePanel("Select folder with options file", "", "txt", false);
        if (pathFile.Length is 0) return;
        ReadOptionsFromFile(pathFile[0]);
    }

    /// <summary>
    /// Read all the options from file 'p' and apply them to the program.
    /// </summary>
    private void ReadOptionsFromFile(string p)
    {
        Color currentColor = Color.black;
        string information = "";
        information = File.ReadAllText(p);
        if (information is "") return;
        List<string> informations = new List<string>(information.Split('?'));
        if (informations[0] is "1") ChangeIfDyslexicFont(true);
        ChangeStorePath(informations[1]);
        ColorUtility.TryParseHtmlString("#"+informations[2], out currentColor);
        SetColorBackground(currentColor);
        currentColor = Color.white;
        ColorUtility.TryParseHtmlString("#"+informations[3], out currentColor);
        SetColorText(currentColor);
    }

    /// <summary>
    /// Apply the colors 'tColor' and 'bColor' to the program text and backgrounds, respectively.
    /// </summary>
    public void ApplyPreset(Color bColor, Color tColor)
    {
        SetColorText(tColor);
        SetColorBackground(bColor);
    }
}
