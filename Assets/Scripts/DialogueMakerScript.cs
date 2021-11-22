using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DialogueMaker.Classes;

/// <summary>
/// This script controls all the main dealing with the json files.
/// </summary>
public class DialogueMakerScript : MonoBehaviour
{
    private int number;
    private int version;

    /// <summary>
    /// Save question 'a' in a new file in path 'path'.
    /// </summary>
    public void SaveQuestionInNewFile(string path, Answer a)
    {
        //Start values
        Conversation c = new Conversation();
        c.version = 1;
        c.dialogue = new List<DialogueLine>();
        c.dialogue.Clear();
        c.answers = new List<Answer>();
        c.answers.Clear();
        //Store values in new instance.
        a.number = 1;
        c.answers.Add(a);
        FullConversation fc = new FullConversation();
        fc.conversations = new List<Conversation>();
        fc.conversations.Clear();
        fc.conversations.Add(c);
        //Create new json wile with this information.
        string json = JsonUtility.ToJson(fc, true);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// We save the question 'a' in the file in path 'path'.
    /// </summary>
    public void SaveQuestionInExistingFile(string path, string n, string v, Answer a)
    {
        bool convExiste = false;
        //Create instance of conversation.
        FullConversation fc = new FullConversation();
        fc.conversations = new List<Conversation>();
        fc.conversations.Clear();
        //Read the information of jsonfile and save it in 'fc'.
        string oldJson = File.ReadAllText(path);
        fc = JsonUtility.FromJson<FullConversation>(oldJson);
        //We add the conversation 'c' to the appropiate version.
        Conversation c = new Conversation();
        c.answers = new List<Answer>();
        if (v == "")
        {
            c = fc.conversations[fc.conversations.Count -1];
        }
        else
        {
            version = int.Parse(v);
            int i = 0;
            while (i < fc.conversations.Count && convExiste == false)
            {
                if (fc.conversations[i].version == version) convExiste = true;
                else i++;
            }
            if (!convExiste)
            {
                c.version = version;
                fc.conversations.Add(c);
            }
            else
            {
                c = fc.conversations[i];
            }
        }
        //We add the answer to the appropiate position in the conversation.
        if (n == "")
        {
            a.number = c.answers.Count +1;
            c.answers.Add(a);
        }
        else
        {
            number = int.Parse(n);
            if (number-1 <= c.dialogue.Count)
            {
                a.number = number;
                c.answers[number-1] = a;
            }
            else
            {
                a.number = c.answers.Count +1;
                c.answers.Add(a);
            }
        }
        ///Parse 'fc' to json again and re-write the file in 'path'.
        string json = JsonUtility.ToJson(fc, true);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Save dialogueLine 'd' in a new file in path 'path'.
    /// </summary>
    public void SaveDialogueInNewFile(string path, DialogueLine d)
    {
        //Start values
        Conversation c = new Conversation();
        c.version = 1;
        c.dialogue = new List<DialogueLine>();
        c.dialogue.Clear();
        c.answers = new List<Answer>();
        c.answers.Clear();
        //Store values in new instance.
        d.number = 1;
        c.dialogue.Add(d);
        FullConversation fc = new FullConversation();
        fc.conversations = new List<Conversation>();
        fc.conversations.Clear();
        fc.conversations.Add(c);
        Debug.Log("Storing dialogue line: " + fc.conversations[0].dialogue[0].number + ", " + fc.conversations[0].dialogue[0].name +
        ", " + fc.conversations[0].dialogue[0].line);
        //Create new json file with thsi information.
        string json = JsonUtility.ToJson(fc, true);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// We save the dialogueLine 'd' in the file in path 'path'.
    /// </summary>
    public void SaveDialogueInExistingFile(string path, string n, string v, DialogueLine d)
    {
        bool convExiste = false;
        //Create instance of conversation.
        FullConversation fc = new FullConversation();
        fc.conversations = new List<Conversation>();
        fc.conversations.Clear();
        //Read json file and store the values.
        string oldJson = File.ReadAllText(path);
        fc = JsonUtility.FromJson<FullConversation>(oldJson);
        //We add the conversation 'c' to the appropiate version.
        Conversation c = new Conversation();
        c.dialogue = new List<DialogueLine>();
        if (v == "")
        {
            c = fc.conversations[fc.conversations.Count -1];
        }
        else
        {
            version = int.Parse(v);
            int i = 0;
            while (i < fc.conversations.Count && convExiste == false)
            {
                if (fc.conversations[i].version == version) convExiste = true;
                else i++;
            }
            if (!convExiste)
            {
                c.version = version;
                fc.conversations.Add(c);
            }
            else
            {
                c = fc.conversations[i];
            }
        }
        //We add the dialogue line to the appropiate position in conversation.
        if (n == "")
        {
            d.number = c.dialogue.Count +1;
            c.dialogue.Add(d);
        }
        else
        {
            number = int.Parse(n);
            if (number-1 <= c.dialogue.Count)
            {
                d.number = number;
                c.dialogue[number-1] = d;
            }
            else
            {
                d.number = c.dialogue.Count +1;
                c.dialogue.Add(d);
            }
        }
        ///Parse 'fc' to json again and re-write the file in 'path'.
        string json = JsonUtility.ToJson(fc, true);
        File.WriteAllText(path, json);
    }
}