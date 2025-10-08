using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Mengelola state dalam satu level, seperti timer.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI timerText; // Teks untuk menampilkan waktu
    private float elapsedTime;
    private bool isTimerRunning;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
        if(Time.deltaTime > 0)
        {
            UpdateTimerText();
        }
    }

    public float StopTimerAndGetTime()
    {
        isTimerRunning = false;
        return elapsedTime;
    }

    public void UpdateTimerText()
    {
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(elapsedTime / 60);
            float seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}