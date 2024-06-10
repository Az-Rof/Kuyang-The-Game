using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class HideScript : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        PlayerbeVisible();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        VisualEffect visualEffect = GetComponent<VisualEffect>();
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player != null && other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if (player.isHiding == false)
            {
                //player is hiding
                player.isHiding = true;

                if (visualEffect != null)
                {
                    visualEffect.enabled = true;
                }
                player.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
        else if (visualEffect != null && !player.isHiding)
        {
            visualEffect.enabled = false;
        }
    }

    void PlayerbeVisible()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player != null && player.isHiding && Input.GetKeyDown(KeyCode.E))
        {
            //player is unhiding
            player.isHiding = false;
            player.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}