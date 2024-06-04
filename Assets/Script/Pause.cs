using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    LiveScript liveScript;
    public GameObject pausePanel;
    public GameObject optionPanel;
    private bool isPaused = false;

    void Start()
    {
        liveScript = GameObject.FindObjectOfType<LiveScript>();
    }
    void Update()
    {
        pause();
    }



    /// Handles pausing/unpausing the game.
    void pause()
    {
        // Check if the Escape key was pressed
        // If the game is currently paused, unpause it
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0 && !optionPanel.activeSelf)
            {
                Time.timeScale = 1;
                isPaused = false;
                // Hide the pause panel if the game is unpaused
                pausePanel.SetActive(false);
            }
            else if (optionPanel.activeSelf)
            {
                optionPanel.SetActive(false);
            }
            // If the game is currently running, pause it
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                // Show the pause panel if the game is paused
                if (isPaused)
                {
                    pausePanel.SetActive(true);
                }
            }
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        // Hide the pause panel if the game is unpaused
        pausePanel.SetActive(false);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void RetryGame()
    {
        if (PlayerPrefs.GetInt("Lives") <= 0)
        {
            liveScript.ResetLives();
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
