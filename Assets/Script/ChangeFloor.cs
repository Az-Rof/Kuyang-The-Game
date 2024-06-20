using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    public GameObject connectFloor;
    public Vector2 newFloorPosition;
    private Rigidbody2D otherRigidbody;

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
        if (other != null && other.tag == "NPC" && other.GetComponent<NPCBehaviour>().wantChangeLevel)
        {
            // StartCoroutine(ChangeLevelWithDelay(other.gameObject));
            other.GetComponent<NPCBehaviour>().wantChangeLevel = false;
            other.transform.position = newFloorPosition;
        }
        if (other != null && other.tag == "NPCNoBaby" && other.GetComponent<NPCBehaviour_NoBaby>().wantChangeLevel)
        {
            // StartCoroutine(ChangeLevelWithDelay(other.gameObject));
            other.GetComponent<NPCBehaviour_NoBaby>().wantChangeLevel = false;
            other.transform.position = newFloorPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.tag == "NPC" && other.GetComponent<NPCBehaviour>().wantChangeLevel)
        {
            // StartCoroutine(ChangeLevelWithDelay(other.gameObject));
            other.GetComponent<NPCBehaviour>().wantChangeLevel = false;
            other.transform.position = newFloorPosition;
        }
        if (other != null && other.tag == "NPCNoBaby" && other.GetComponent<NPCBehaviour_NoBaby>().wantChangeLevel)
        {
            // StartCoroutine(ChangeLevelWithDelay(other.gameObject));
            other.GetComponent<NPCBehaviour_NoBaby>().wantChangeLevel = false;
            other.transform.position = newFloorPosition;
        }
    }

    // IEnumerator ChangeLevelWithDelay(GameObject other)
    // {
    //     other.GetComponent<SpriteRenderer>().enabled = false;
    //     yield return new WaitForSeconds(1.5f);
    //     other.GetComponent<NPCBehaviour>().wantChangeLevel = false;
    //     other.transform.position = newFloorPosition;
    //     other.GetComponent<SpriteRenderer>().enabled = true;
    // }
}