using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiveScript : MonoBehaviour
{

    //Lives needed !
    [SerializeField] TextMeshProUGUI livesText, livesLeft, CollectedCollectable;
    // [SerializeField] bool initLive = false;
    [SerializeField] GameObject GameOver;
    public static int lives
    {
        get; internal set;
    }
    void Start()
    {
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

        GameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        // if (initLive == true)
        // {
        //     ResetLives();
        //     initLive = false;
        // }
        livesText.text = PlayerPrefs.GetInt("Lives") + " / 5";
        livesLeft.text = "Lives Left :   " + PlayerPrefs.GetInt("Lives") + " / 5";
        CollectedCollectable.text = "Bloods Collected in this level :   " + Collectables.collectedCollectables + " / " + Collectables.totalCollectables;
    }
}
