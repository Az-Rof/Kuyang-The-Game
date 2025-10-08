using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class FinishLevel : MonoBehaviour
{
    private int level;
    [SerializeField] GameObject player, baby;
    public GameObject levelfinish;
    public GameObject NextUpdate;
    public BabyScript babyScript;

    void Awake()
    {
        findPlayer();
        findBaby();
    }

    void findPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            return;
        }
    }

    void findBaby()
    {
        if (baby == null)
        {
            baby = GameObject.FindObjectOfType<BabyScript>(true)?.gameObject;
            babyScript = baby.GetComponent<BabyScript>();
        }
        else
        {
            return;
        }
    }
    private void Start()
    {
        // Extract the level number from the scene name
        string sceneName = SceneManager.GetActiveScene().name;
        level = int.Parse(sceneName.Replace("Level ", ""));
        AudioManager.Instance.LsfxSource.Stop();
    }
    void Update()
    {

    }

    public void NextLevel()
    {
        if (babyScript != null && babyScript.isKidnapped)
        {
            CompleteLevel();
            Time.timeScale = 0f;
            levelfinish.SetActive(true);
            AudioManager.Instance.LsfxSource.Stop();
        }
    }

    private void CompleteLevel()
    {
        float finalTime = LevelManager.Instance.StopTimerAndGetTime();

        GameData gameData = SaveManager.LoadGame() ?? new GameData();

        // Cari data untuk level saat ini
        LevelData currentLevelData = gameData.levelDataList.Find(ld => ld.levelNumber == level);
        if (currentLevelData == null) // Jika tidak ada, ini aneh, tapi kita buat saja
        {
            currentLevelData = new LevelData { levelNumber = level, isUnlocked = true };
            gameData.levelDataList.Add(currentLevelData);
        }

        // Update waktu terbaik jika waktu saat ini lebih cepat, atau jika belum ada waktu
        if (currentLevelData.bestTime < 0 || finalTime < currentLevelData.bestTime)
        {
            currentLevelData.bestTime = finalTime;
            currentLevelData.bloodCollected = Collectables.collectedCollectables;
            currentLevelData.totalBlood = Collectables.totalCollectables;
        }

        // Buka level selanjutnya
        int nextLevel = level + 1;
        LevelData nextLevelData = gameData.levelDataList.Find(ld => ld.levelNumber == nextLevel);
        if (nextLevelData == null)
        {
            gameData.levelDataList.Add(new LevelData { levelNumber = nextLevel, isUnlocked = true });
        }
        else
        {
            nextLevelData.isUnlocked = true;
        }

        SaveManager.SaveGame(gameData);
    }

    public void LoadNextLevel()
    {
        int nextLevel = level + 1;
        Time.timeScale = 1f;
        if (Application.CanStreamedLevelBeLoaded("Level " + nextLevel))
        {
            SceneManager.LoadScene("Level " + nextLevel);
        }
        else
        {
            // Jika tidak ada, mungkin tampilkan panel "Coming Soon"
            if (NextUpdate != null)
            {
                NextUpdate.SetActive(true);
            }
        }
    }
}