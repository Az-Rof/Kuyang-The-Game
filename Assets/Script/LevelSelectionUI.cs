using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Mengelola UI untuk menu pemilihan level.
/// </summary>
public class LevelSelectionUI : MonoBehaviour
{
    public GameObject levelButtonPrefab; // Prefab tombol level yang Anda buat
    public Transform buttonContainer;    // Panel/Layout Group tempat tombol akan dibuat

    void Start()
    {
        PopulateLevelButtons();
    }

    void PopulateLevelButtons()
    {
        GameData gameData = SaveManager.LoadGame();

        if (gameData == null || gameData.levelDataList.Count == 0)
        {
            Debug.LogWarning("No game data found. Cannot populate level selection.");
            return;
        }

        // Urutkan level berdasarkan nomornya
        var sortedLevels = gameData.levelDataList.OrderBy(l => l.levelNumber);

        foreach (LevelData levelData in sortedLevels)
        {
            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonContainer);
            Button levelButton = buttonGO.GetComponent<Button>();

            // Atur teks pada tombol
            // Asumsi nama komponen TextMeshPro di prefab adalah "LevelText", "TimeText", "BloodText"
            buttonGO.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = $"Level {levelData.levelNumber}";

            TextMeshProUGUI timeText = buttonGO.transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI bloodText = buttonGO.transform.Find("BloodText").GetComponent<TextMeshProUGUI>();

            if (levelData.isUnlocked)
            {
                levelButton.interactable = true;
                int levelNum = levelData.levelNumber; // Capture a local copy for the listener
                levelButton.onClick.AddListener(() => SceneManager.LoadScene($"Level {levelNum}"));

                if (levelData.bestTime >= 0) // Cek apakah level sudah pernah diselesaikan
                {
                    float minutes = Mathf.FloorToInt(levelData.bestTime / 60);
                    float seconds = Mathf.FloorToInt(levelData.bestTime % 60);
                    timeText.text = $"Best Time: {minutes:00}:{seconds:00}";
                    bloodText.text = $"Total Bloods Collected: {levelData.bloodCollected}/{levelData.totalBlood}";
                }
            }
            else
            {
                levelButton.interactable = false;
            }
        }
    }
}