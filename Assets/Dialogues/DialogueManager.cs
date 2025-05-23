using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*Queue basically runs on principle of FIFO(First in First Out)
     *So the Dialogues that users read will come and go suits this
     *since all dialogues are strings, the datatype attaced is string.
     */
    private Queue<string> sentences;

    public Text DialogueText;
    public bool isenddialogue;
    private void Awake() 
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogues dialogue) 
    {
        isenddialogue = false;
        sentences.Clear();//to clear all the previous dialogue if any;

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);//Queues all the dialogues.
        }

        DisplayNextSentence();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        //DialogueText.text = sentence;
        StopAllCoroutines();//Ensures that if users skips throught the senteces.
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence) 
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            DialogueText.text += letter;
            yield return null;//waits for single frame(doesnot wait.)
        }
    }

    public void EndDialogue() 
    {
        isenddialogue = true;
        SceneManager.LoadScene(2);
    }

}
