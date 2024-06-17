using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    public GameObject connectFloor;
    public Vector2 newFloorPosition;

    void Start()
    {
        newFloorPosition = connectFloor.transform.position;
    }

    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            other.transform.position = newFloorPosition;
        }
        if (other.tag == "NPC" && other.GetComponent<NPCBehaviour>().wantChangeLevel)
        {
            other.transform.position = newFloorPosition;
            other.GetComponent<NPCBehaviour>().wantChangeLevel = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC" && other.GetComponent<NPCBehaviour>().wantChangeLevel)
        {
            other.transform.position = newFloorPosition;
            other.GetComponent<NPCBehaviour>().wantChangeLevel = false;
        }
    }
}