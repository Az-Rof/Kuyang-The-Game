using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiveScript : MonoBehaviour
{
    public static LiveScript Instance { get; private set; }

    //Lives needed !
    [SerializeField] TextMeshProUGUI livesText, livesLeft, livesLeft2, CollectedCollectable, CollectedCollectable2;

    public GameObject GameOver;

    void Start()
    {
        // Initial UI update
        UpdateUI();
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        livesText = GetComponent<TextMeshProUGUI>();
    }

    public void ResetLives()
    {
        PlayerPrefs.SetInt("Lives", 5);
    }

    public void ReduceLives()
    {
        int lives = PlayerPrefs.GetInt("Lives");
        lives = lives - 1;
        PlayerPrefs.SetInt("Lives", lives);

        UpdateUI();

        GameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateUI()
    {
        livesText.text = PlayerPrefs.GetInt("Lives") + " / 5";
        livesLeft.text = "Lives Left :   " + PlayerPrefs.GetInt("Lives") + " / 5";
        livesLeft2.text = "Lives Left :   " + PlayerPrefs.GetInt("Lives") + " / 5";
        CollectedCollectable.text = "Bloods Collected in this level :   " + Collectables.collectedCollectables + " / " + Collectables.totalCollectables;
        CollectedCollectable2.text = "Bloods Collected in this level :   " + Collectables.collectedCollectables + " / " + Collectables.totalCollectables;
    }
}
