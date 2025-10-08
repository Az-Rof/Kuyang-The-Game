using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds, Lsfxsounds;
    public AudioSource musicSource, sfxSource, LsfxSource;

    private Dictionary<string, Sound> musicSoundsDict;
    private Dictionary<string, Sound> sfxSoundsDict;
    private Dictionary<string, Sound> lsfxSoundsDict;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize dictionaries for fast lookups
        musicSoundsDict = musicSounds.ToDictionary(s => s.name, s => s);
        sfxSoundsDict = sfxSounds.ToDictionary(s => s.name, s => s);
        lsfxSoundsDict = Lsfxsounds.ToDictionary(s => s.name, s => s);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Load volume settings every time, as AudioSources might be new
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            // If the music volume is found, set it
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            // If the sfx volume is found, set it
            sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume");
        }

        // Scene-specific audio logic
        if (scene.name == "Main Menu")
        {
            playMusic("MainMenu Music");
            LsfxSource.Stop();
        }
        else if (scene.name == "CutScene_Intro")
        {
            musicSource.Stop();
            LsfxSource.Stop();
        }
    }

    public void Start() { /* Start can be kept for initialization if needed, but OnSceneLoaded is better for scene-specific logic */ }

    /// Plays the music with the specified name.
    /// <param name="name">The name of the music sound.</param>
    public void playMusic(string name)
    {
        if (!musicSoundsDict.TryGetValue(name, out Sound s))
        {
            Debug.LogWarning($"Music '{name}' not found in dictionary!");
            return;
        }

        if (s.audioClip == null)
        {
            Debug.LogWarning($"Music '{name}' has no AudioClip assigned!");
            return;
        }

        musicSource.clip = s.audioClip;
        musicSource.Play();
    }


    // Plays a sound effect based on the passed name. If the sound is not found in the musicSounds array, logs a message. 
    // Otherwise, plays the found sound effect using the sfxSource.
    public void playSFX(string name)
    {
        if (!sfxSoundsDict.TryGetValue(name, out Sound s))
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
            // sfxSource.clip = s.audioClip;
            // sfxSource.Play();
        }
    }
    public void playLSFX(string name)
    {
        if (!lsfxSoundsDict.TryGetValue(name, out Sound s))
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            LsfxSource.clip = s.audioClip;
            LsfxSource.Play();
        }
    }

    public void stopSFX(string name)
    {
        if (!sfxSoundsDict.TryGetValue(name, out Sound s))
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.Stop();
        }
    }

    /// Adjusts the volume of the music source
    public void musicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void sfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
