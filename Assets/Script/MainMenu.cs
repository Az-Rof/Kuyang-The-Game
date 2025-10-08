using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Manage UI Pop-Up & Button Clicks
    // (The name of Button and Pop-Up must be the same)
    public List<GameObject> PopUp;
    public List<Button> ClickedButton;
    public Button ContinueButton; // Tambahkan referensi ke tombol Continue di Inspector

    private GameData gameData;

    void Start()
    {
        // Deactivate all pop-ups at the start
        foreach (GameObject popUp in PopUp)
        {
            if (popUp != null)
            {
                popUp.SetActive(false);
            }
            
        }
        // Add a listener for each button
        foreach (Button button in ClickedButton)
        {
            if (button != null)
            {
                // Use a lambda expression to pass the button's name to the listener
                button.onClick.AddListener(() => ListenButtonClick(button.gameObject.name));
            }
        }

        // Muat data game dan atur tombol Continue
        LoadAndSetupContinueButton();
    }

    void LoadAndSetupContinueButton()
    {
        gameData = SaveManager.LoadGame();
        if (ContinueButton != null)
        {
            // Aktifkan tombol Continue hanya jika ada save data dan level yang terbuka lebih dari 1
            // Cek apakah ada level selain level 1 yang sudah terbuka
            bool canContinue = gameData != null && gameData.levelDataList.Exists(l => l.isUnlocked && l.levelNumber > 1);
            ContinueButton.interactable = canContinue;
            ContinueButton.onClick.AddListener(ContinueGame);
        }
    }

    public void ListenButtonClick(string buttonName)
    {
        // Find the matching pop-up and activate it, deactivating others
        foreach (GameObject popUp in PopUp)
        {
            if (popUp != null)
            {
                popUp.SetActive(popUp.name == buttonName);
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
        // Hapus save data lama jika ada, atau cukup timpa saat save baru
        // Untuk game baru, kita buat data baru dimana hanya level 1 yang terbuka
        GameData newGameData = new GameData();
        newGameData.levelDataList.Add(new LevelData 
        { 
            levelNumber = 1, 
            isUnlocked = true 
        });
        SaveManager.SaveGame(newGameData);

        PlayerPrefs.SetInt("Lives", 5);
        SceneManager.LoadScene("CutScene_Intro");
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        if (gameData != null && gameData.levelDataList.Count > 0)
        {
            // Cari level tertinggi yang sudah terbuka
            int highestLevel = gameData.levelDataList
                .Where(l => l.isUnlocked)
                .Max(l => l.levelNumber);

            SceneManager.LoadScene("Level " + highestLevel);
            Time.timeScale = 1;
        }
    }
    #endregion
}