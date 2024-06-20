using System.Timers;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BabyScript : MonoBehaviour
{
    public bool isSleeping = true;
    public float minSleepDuration = 40f; // Minimmum sleep duration in seconds
    public float maxSleepDuration = 120f; // Maximum sleep duration in seconds 
    public float minAwakeDuration = 5f; // Minimum awake duration in seconds
    public float maxAwakeDuration = 15f; // Maximum awake duration in seconds
    private float sleepTimer = 0f;
    private float awakeTimer = 0f; // Timer for when the baby is awake

    // [SerializeField] AudioClip clip;
    // AudioSource CrySFX;

    Light2D light2D;
    public bool isKidnapped = false;

    Animator animator;

    void Start()
    {
        sleepTimer = UnityEngine.Random.Range(minSleepDuration, maxSleepDuration);
        light2D = GetComponent<Light2D>();
        // CrySFX = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {

        if (isSleeping)
        {
            sleepTimer -= Time.deltaTime;


            if (sleepTimer <= 0)
            {
                isSleeping = false;
                awakeTimer = UnityEngine.Random.Range(minAwakeDuration, maxAwakeDuration);
            }

            AudioManager.Instance.playLSFX("Baby Cry");
            // looping playlsfx
            AudioManager.Instance.LsfxSource.loop = true;
        }
        else if (!isSleeping)
        {
            awakeTimer -= Time.deltaTime;

            if (awakeTimer <= 0)
            {
                isSleeping = true;
                sleepTimer = UnityEngine.Random.Range(minSleepDuration, maxSleepDuration);
            }

            //AudioManager.Instance.LsfxSource.Stop();
            //AudioManager.Instance.LsfxSource.loop = false;

        }

        if (isKidnapped)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().transform.position;
            isSleeping = false;
        }
        Animator();

    }
    void OnTriggerStay2D(Collider2D other)
    {
        bool isHiding = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isHiding;
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            transform.Find("ButtonE").gameObject.SetActive(false);
            isKidnapped = true;
            light2D.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.Find("ButtonE").gameObject.SetActive(true);
            light2D.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.Find("ButtonE").gameObject.SetActive(false);
            light2D.enabled = false;
        }
    }

    void Animator()
    {
        // Jika NPC sedang idle
        if (isSleeping)
        {
            animator.SetBool("isSleeping", true);

        }
        else // Jika NPC tidak idle (sedang berjalan)
        {
            animator.SetBool("isSleeping", false);
        }
    }
}