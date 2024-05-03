using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

   public void Exit() {
        Application.Quit();
   }
}

