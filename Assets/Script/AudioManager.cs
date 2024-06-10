using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
    }
    /// <summary>
    /// Plays the music for the specified scene when it starts.
    /// </summary>
    public void Start()
    {
        // Retrieve the music and sfx volumes from PlayerPrefs if they exist
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
        // Check which scene is currently active
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            // If the scene is the main menu, play the Main Menu Theme music
            playMusic("MainMenu Theme");
        }
        else
        {
            // If the scene is not the main menu, do not play music
        }
    }


    /// Plays the music with the specified name.
    /// <param name="name">The name of the music sound.</param>
    public void playMusic(string name)
    {
        // Find the sound with the specified name
        Sound s = Array.Find(musicSounds, x => x.name == name);

        // If the sound is not found, log a message
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            // Set the clip of the music source to the audio clip of the sound
            musicSource.clip = s.audioClip;
            // Play the music
            musicSource.Play();
        }
    }

    // Plays a sound effect based on the passed name. If the sound is not found in the musicSounds array, logs a message. 
    // Otherwise, plays the found sound effect using the sfxSource.
    public void playSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
    public void stopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
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
