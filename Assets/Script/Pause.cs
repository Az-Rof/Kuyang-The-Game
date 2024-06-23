using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    LiveScript liveScript;
    public GameObject pausePanel;
    public GameObject optionPanel, tutorialPanel;
    private bool isPaused = false;

    void Awake()
    {
        liveScript = GameObject.FindObjectOfType<LiveScript>();
        if (liveScript == null)
        {
            Debug.LogError("No LiveScript found in the scene.");
        }
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
        if (Input.GetKeyDown(KeyCode.Escape) && !liveScript.GameOver.activeSelf)
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
                if (tutorialPanel.activeSelf)
                {
                    tutorialPanel.SetActive(false);
                }
                else
                {
                    optionPanel.SetActive(false);
                }

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
        LiveScript liveScript = GameObject.FindObjectOfType<LiveScript>();
        if (liveScript != null)
        {
            int lives = PlayerPrefs.GetInt("Lives");
            if (lives <= 0)
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
        else
        {
            Debug.LogError("LiveScript is null.");
        }
    }
    public void pauseButton()
    {
        if (!liveScript.GameOver.activeSelf)
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
                if (tutorialPanel.activeSelf)
                {
                    tutorialPanel.SetActive(false);
                }
                else
                {
                    optionPanel.SetActive(false);
                }

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
    public void settingButton()
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        if (optionPanel.activeSelf)
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }
            else
            {
                optionPanel.SetActive(true);
            }
        }
    }
}
