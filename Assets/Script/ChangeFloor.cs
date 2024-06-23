using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    public GameObject connectFloor;
    Vector2 newFloorPosition;
    public int order;
    private const float NPC_COOLDOWN = 2f;
    private Dictionary<NPCBehaviour, float> npcCooldowns = new Dictionary<NPCBehaviour, float>();

    void Start()
    {
        newFloorPosition = connectFloor.transform.position;
    }

    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            other.transform.position = newFloorPosition;
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

    IEnumerator ChangeFloorForNPC(NPCBehaviour npc)
    {

        npc.transform.position = newFloorPosition;

        // Tambahkan cooldown
        npc.cooldown = NPC_COOLDOWN;

        // Tunggu sebentar sebelum mengizinkan NPC bergerak lagi
        yield return new WaitForSeconds(0.5f);
        npc.ischanginglevel = false;
    }
}