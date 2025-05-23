using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionnewlevel : MonoBehaviour
{
    public DialogueManager manager;
    private bool playerInTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Make sure it's the player
        {
            playerInTrigger = true;
        }
    }

    private void Update()
    {
        // Check if player is in trigger, dialogue has ended, and F key is pressed
        if (playerInTrigger && manager.isenddialogue && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(2);
        }
    }
}
