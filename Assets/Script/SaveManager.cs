using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Kelas statis untuk menangani penyimpanan dan pemuatan data game ke file JSON.
/// </summary>
public static class SaveManager
{
    // Path ke file save, disimpan di lokasi yang persisten untuk setiap platform.
    private static string saveFilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");

    /// <summary>
    /// Menyimpan data game ke file JSON.
    /// </summary>
    public static void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true); // 'true' untuk format yang mudah dibaca
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved to: " + saveFilePath);
    }

    /// <summary>
    /// Memuat data game dari file JSON. Mengembalikan null jika tidak ada file save.
    /// </summary>
    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game Loaded from: " + saveFilePath);
            return data;
        }
        return null;
    }
}

/// <summary>
/// Kelas untuk menampung data yang akan disimpan.
/// </summary>
[System.Serializable]
public class GameData
{
    public List<LevelData> levelDataList = new List<LevelData>();
}

/// <summary>
/// Kelas untuk menampung data per level.
/// </summary>
[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public bool isUnlocked;

    // -1f menandakan level belum pernah diselesaikan
    public float bestTime = -1f;
    public int bloodCollected;
    public int totalBlood;
}