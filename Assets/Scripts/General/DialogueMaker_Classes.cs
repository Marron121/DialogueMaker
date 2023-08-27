using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds all the classes used in the program 'Dialogue Maker'.
/// </summary>
namespace DialogueMaker.Classes
{
    [System.Serializable]
    public class DialogueLine
    {
        public int number;
        public string name;
        public string line;
        public string emotion;
    }

    [System.Serializable]
    public class Answer
    {
        public int number;
        public string line;
    }

    [System.Serializable]
    public class Conversation
    {
        public int version;
        public List<DialogueLine> dialogue;
        public List<Answer> answers;
    }

    [System.Serializable]
    public class FullConversation
    {
        public List<Conversation> conversations;
    }
}
