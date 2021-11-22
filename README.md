# DialogueMaker
A program to quickly write dialogues in a JSON format.
## What is Dialogue Maker?
Dialogue Maker is a program I used to write the dialogues for the game I created alongside friends as a TFG (Spain's thesis for the degree). I upgraded the work I did there, cleaned it up a bit and decided to release it as open-source, with the hopes it might be useful to anyone out there who needs it.
## What can Dialogue Maker do?
In the version 1.0 (the one I'm releasing), with Dialogue Maker you can:
* Automatically create JSON files with lines of dialogue and questions.
* Allow the creation of different versions of the same conversation, in a single JSON file.
* Define in each line who talks and the emotion of the line.
* Check the current file you're writing.
* Overwrite any line/question simply stating the number of the line/question you want to fix.
## How are dialogues divided in classes
* Each JSON file is composed of a single FullConversation, which contains a list of Conversations.
* Each Conversation is formed with a number, a list of DialogueLines and a list of Answers.
```csharp
    public class FullConversation
    {
        public List<Conversation> conversations;
    }
    
    public class Conversation
    {
        public int version;
        public List<DialogueLine> dialogue;
        public List<Answer> answers;
    }
    
    public class DialogueLine
    {
        public int number;
        public string name;
        public string line;
        public string emotion;
    }
    
    public class Answer
    {
        public int number;
        public string line;
    }
```
## Other info
* Unity version: 2020.3.3f1.
* [Check the PDF Guide](https://github.com/Marron121/DialogueMaker/blob/67e4434578a7ebc4f4302063ede81279fadc7c8a/Dialogue%20Maker%20Guide.pdf).
* [Download an executable of the program v1.0](https://drive.google.com/file/d/1WHBhN7x1wyKPjr_8fuiZ8Ry4chA24oiN/view?usp=sharing).
