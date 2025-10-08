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

    bool playerInteract;

    Light2D light2D;
    public bool isKidnapped = false;

    Animator animator;

    // --- Cached References ---
    private Transform playerTransform;
    private GameObject buttonE;

    void Start()
    {
        sleepTimer = UnityEngine.Random.Range(minSleepDuration, maxSleepDuration);
        light2D = GetComponent<Light2D>();
        animator = GetComponent<Animator>();

        // Cache player transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        // Cache ButtonE game object
        Transform buttonETransform = transform.Find("ButtonE");
        if (buttonETransform != null)
        {
            buttonE = buttonETransform.gameObject;
            buttonE.SetActive(false); // Make sure it's off at the start
        }

        UpdateState();
    }

    void Update()
    {
        playerKidnapp();
        if (isKidnapped)
        {
            if (playerTransform != null)
            {
                transform.position = playerTransform.position;
            }
            return; // No need to process sleep/awake logic if kidnapped
        }

        sleepTimer -= Time.deltaTime;

        if (sleepTimer <= 0)
        {
            isSleeping = !isSleeping;
            UpdateState();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (isKidnapped) return;

        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isKidnapped) return;

        if (other.CompareTag("Player"))
        {
            playerInteract = true;
            if (buttonE != null)
            {
                buttonE.SetActive(true);
            }
            light2D.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteract = false;
            if (buttonE != null)
            {
                buttonE.SetActive(false);
            }
            light2D.enabled = false;
        }
    }

    void UpdateState()
    {
        if (isSleeping)
        {
            sleepTimer = UnityEngine.Random.Range(minSleepDuration, maxSleepDuration);
            if (this.gameObject.activeSelf)
            {
                AudioManager.Instance.LsfxSource.Stop(); // Stop previous sound if any
                AudioManager.Instance.LsfxSource.loop = false;
            }
        }
        else // isAwake
        {
            sleepTimer = UnityEngine.Random.Range(minAwakeDuration, maxAwakeDuration);
            if (!AudioManager.Instance.LsfxSource.isPlaying || AudioManager.Instance.LsfxSource.clip.name != "Baby Cry")
            {
                AudioManager.Instance.playLSFX("Baby Cry");
                AudioManager.Instance.LsfxSource.loop = true;
            }
        }

        // Update animator
        animator.SetBool("isSleeping", isSleeping);
    }

    void playerKidnapp()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInteract && !isKidnapped)
        {
            isKidnapped = true;
            isSleeping = false; // Baby is awake when kidnapped
            UpdateState();

            if (buttonE != null)
            {
                buttonE.SetActive(false);
            }
            light2D.enabled = false;
        }
    }

    public void StopBabyCry()
    {
        AudioManager.Instance.LsfxSource.Stop();
    }
}