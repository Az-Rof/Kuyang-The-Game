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
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().isInvisible == false)
            {
                liveScript.ReduceLives();
            }
        }
    }
}