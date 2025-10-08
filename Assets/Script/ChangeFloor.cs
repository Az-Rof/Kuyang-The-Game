using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    public GameObject connectFloor;
    Vector2 newFloorPosition;
    public int order;
    private const float NPC_COOLDOWN = 2f;

    bool playerInteract;

    void Start()
    {
        newFloorPosition = connectFloor.transform.position;
    }

    void Update()
    {
        playerchangefloor();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInteract = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
        else if (other.CompareTag("NPC"))
        {
            NPCBehaviour npc = other.GetComponent<NPCBehaviour>();
            if (npc != null && npc.wantChangeLevel && npc.ischanginglevel && npc.cooldown <= 0)
            {
                StartCoroutine(ChangeFloorForNPC(npc));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteract = false;
        }
    }

    IEnumerator ChangeFloorForNPC(NPCBehaviour npc)
    {
        npc.transform.position = newFloorPosition;

        // Tambahkan cooldown
        npc.cooldown = NPC_COOLDOWN;

        // Tunggu sebentar sebelum mengizinkan NPC bergerak lagi
        yield return new WaitForSeconds(0.5f);
        npc.ischanginglevel = false;
    }

    void playerchangefloor()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInteract)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.transform.position = newFloorPosition;
        }
    }
}