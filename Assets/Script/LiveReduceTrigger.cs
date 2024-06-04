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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            liveScript.ReduceLives();
        }
    }
}