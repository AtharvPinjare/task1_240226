using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("GAME OVER")]
    [SerializeField] private GameObject gameOverscreen;
    [SerializeField]private AudioClip GameOverSound;

    [Header("PAUSE")]
    [SerializeField] private GameObject pausescreen;
    //[SerializeField] private AudioClip pauseSound;

    private void Awake()
    {
        gameOverscreen.SetActive(false);
        pausescreen.SetActive(false);
    }

    #region GAME OVER
    public void GameOver() //Activate GameOver Screen.
    {
        gameOverscreen.SetActive(true);
        SoundManager.instance.PlaySound(GameOverSound); 

    }


    //Assigning the onclicked Functions.
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void Exit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If Pause screen is alreay active, it toggles it.
            if (pausescreen.activeInHierarchy) { TogglePauseScreen(false); }
            else { TogglePauseScreen(true); }
        }

    }
    public void TogglePauseScreen(bool _status)
    {
        pausescreen.SetActive(_status);//toggles pauses screen based on the status.

        //Timescale 0 means everything is paused
        if (_status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
