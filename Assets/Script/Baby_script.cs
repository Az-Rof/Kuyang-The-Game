using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby_script : MonoBehaviour
{
    public bool isSleeping = true;
    public float minSleepDuration = 40f; // Minimmum sleep duration in seconds
    public float maxSleepDuration = 120f; // Maximum sleep duration in seconds 
    public float minAwakeDuration = 5f; // Minimum awake duration in seconds
    public float maxAwakeDuration = 15f; // Maximum awake duration in seconds
    private float sleepTimer = 0f;
    private float awakeTimer = 0f; // Timer for when the baby is awake

    public bool isKidnapped = false;

    void Start()
    {
        // sleepTimer = Random.Range(minSleepDuration, maxSleepDuration);

    }

    void Update()
    {
        // if (isSleeping)
        // {
        //     sleepTimer -= Time.deltaTime;

        //     if (sleepTimer <= 0)
        //     {
        //         isSleeping = false;
        //         awakeTimer = Random.Range(minAwakeDuration, maxAwakeDuration);

        //     }
        // }
        // else
        // {
        //     awakeTimer -= Time.deltaTime;

        //     if (awakeTimer <= 0)
        //     {
        //         isSleeping = true;
        //         sleepTimer = Random.Range(minSleepDuration, maxSleepDuration);

        //     }
        // }

        if (isKidnapped)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().transform.position;
            isSleeping = false;
            //AudioManager.Instance.playSFX("Baby Cry");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            isKidnapped = true;
        }
    }
}