using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShineOnApproach : MonoBehaviour
{
    public GameObject player;
    
    Light2D light2D;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        light2D = GetComponent<Light2D>();
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player){
            GetComponent<Collider2D>().enabled = false;
            light2D.enabled = true;
        }
    }

    private void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            GetComponent<Collider2D>().enabled = true;
            light2D.enabled = false;
        }
    }
}