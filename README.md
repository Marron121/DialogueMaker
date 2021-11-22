# DialogueMaker
A program to quickly write dialogues in a JSON format.
## What is Dialogue Maker?
Dialogue Maker is a program I used to write the dialogues for the game I created alongside friends as a TFG (Spain's thesis for the degree). I upgraded the work I did there, cleaned it up a bit and decided to release it as open-source.
## What can Dialogue Maker do?
In the version 1.0 (the one I'm releasing), with Dialogue Maker you can:
* Automatically create JSON files with lines of dialogue and questions.
* Allow the creation of different versions of the same conversation, in a single JSON file.
* Define in each line who talks and the emotion of the line.
* Check the current file you're writing.
* Overwrite any line/question simply stating the number of the line/question you want to fix.
For more information and an example, please check the following PDF.
## How are dialogues divided in classes
* Each JSON file is composed of a single FullConversation, which contains a list of Conversations.
* Each Conversation is formed with a number, a list of DialogueLines and a list of Answers.
