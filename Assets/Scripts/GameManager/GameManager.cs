using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<Health> enemies = new List<Health>();
  
    [SerializeField] private GameObject gameOverUI; // Optional: UI panel to show

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        // Find all enemies with the "Enemy" tag and get their Health components
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("LEnemy");
        foreach (GameObject enemy in foundEnemies)
        {
            if (enemy.activeInHierarchy)
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemies.Add(enemyHealth);
                }
            }
        }

        Debug.Log($"Found {enemies.Count} enemies to track");
    }

    // Call this method when an enemy's gameObject becomes inactive (dies)
    public void OnEnemyDestroyed(Health enemyHealth)
    {
        if (enemies.Contains(enemyHealth))
        {
            enemies.Remove(enemyHealth);
            Debug.Log($"Enemy destroyed. Remaining: {enemies.Count}");

            CheckGameEnd();
        }
    }

    private void CheckGameEnd()
    {
        if (enemies.Count <= 0)
        {
            Debug.Log("All enemies defeated! Game Over!");
            EndGame();
        }
    }

    private void EndGame()
    {
        // Option 1: Load a game over scene
        // SceneManager.LoadScene(gameOverScene);

        // Option 2: Show game over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Option 3: Pause the game
        Time.timeScale = 0f;

        // Option 4: Just log for now
        Debug.Log("GAME ENDED - ALL ENEMIES DEFEATED!");
    }

}