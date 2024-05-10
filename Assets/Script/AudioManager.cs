using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds,sfxSounds;
    public AudioSource musicSource,sfxSource;

    public void Awake(){
        if(Instance == null){
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        playMusic("Theme"); 
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
    public void playSFX () {
        Sound s = Array.Find(musicSounds,x=>x.name==name);
        if (s==null){
            Debug.Log("Sound Not Found");
        }else{
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
    /// Adjusts the volume of the music source
    public void musicVolume(float volume) {
        musicSource.volume=volume;
    }
    public void sfxVolume(float volume) {
        sfxSource.volume=volume;
    }

}
