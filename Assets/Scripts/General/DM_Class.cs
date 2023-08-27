using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds all the classes used in the program 'Dialogue Maker'.
/// </summary>
namespace DM.JSON
{
    [System.Serializable]
    public class DialogueLine
    {
        public int Number;
        public string Speaker;
        public string Line;
        public string Emotion;
    }

    [System.Serializable]
    public class Answer
    {
        public int Number;
        public string Line;
    }

    [System.Serializable]
    public class Version
    {
        public int Number;
        public List<DialogueLine> DialogueLines;
        public List<Answer> Answers;
    }

    [System.Serializable]
    public class Dialogue
    {
        public List<Version> Versions;
    }
}
