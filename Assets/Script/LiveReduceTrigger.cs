using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveReduceTrigger : MonoBehaviour
{
    LiveScript liveScript;


    private void Start()
    {
        liveScript = GameObject.FindObjectOfType<LiveScript>();

    }
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && other.CompareTag("Player") && !other.GetComponent<PlayerController>().isHiding)
        {
            if (!other.GetComponent<PlayerController>().isInvisible || !player.GetComponent<PlayerController>().isHiding)
            {
                liveScript.ReduceLives();
            }
        }
    }
}