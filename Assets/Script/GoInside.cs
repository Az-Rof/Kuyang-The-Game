using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseInteraction : MonoBehaviour
{
    public GameObject Player, Ext, Int;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");

    }


    void OnTriggerStay2D(Collider2D other)
    {
        // When collider collides with player and player presses a button
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            if (Ext.activeSelf)
            {
                Ext.gameObject.SetActive(false);
                Int.gameObject.SetActive(true);
            }
            else
            {
                Ext.gameObject.SetActive(true);
                Int.gameObject.SetActive(false);
            }
        }
    }
}