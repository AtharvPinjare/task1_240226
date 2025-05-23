using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogue;
    [SerializeField] private bool triggerOnStart; // Toggle in inspector

    private void Start() // Changed to Start to ensure DialogueManager exists
    {
        if (triggerOnStart)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("DialogueManager not found in the scene! Make sure there's a GameObject with DialogueManager script.");
        }
    }
}
