using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInteractable : MonoBehaviour
{
    Light2D light2D;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.Find("ButtonE").gameObject.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        bool isHiding = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isHiding;
        if (other.tag == "Player")
        {
            if (isHiding)
            {
                light2D.enabled = false;
            }
            else if (!isHiding)
            {
                light2D.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            light2D.enabled = false;
            transform.Find("ButtonE").gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }
}


