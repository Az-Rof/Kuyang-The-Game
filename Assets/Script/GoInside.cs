using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseInteraction : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        // When collider collides with player and player presses a button
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            if(GameObject.Find("Rumah1").transform.Find("Exterior").gameObject.activeSelf)
            {
                GameObject.Find("Rumah1").transform.Find("Exterior").gameObject.SetActive(false);
                GameObject.Find("Rumah1").transform.Find("Interior").gameObject.SetActive(true);
            }else
            {
                GameObject.Find("Rumah1").transform.Find("Exterior").gameObject.SetActive(true);
                GameObject.Find("Rumah1").transform.Find("Interior").gameObject.SetActive(false);
            }
        }   
    }
}