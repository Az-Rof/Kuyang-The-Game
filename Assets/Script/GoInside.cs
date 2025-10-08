using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nama file adalah GoInside.cs, tetapi nama kelas adalah HouseInteraction.
// Sebaiknya nama file dan kelas sama untuk konsistensi.
public class GoInside : MonoBehaviour
{
    public GameObject Player, Ext, Int;
    public FinishLevel FinishLevel { get; set; }


    bool playerInteract;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        FinishLevel = GetComponent<FinishLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractedPlayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When collider collides with player
        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // When collider collides with player
        if (other.CompareTag("Player"))
        {
            playerInteract = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // When collider collides with player
        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
    }

    void InteractedPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInteract && !FinishLevel.babyScript.isKidnapped)
        {
            // If player is interacting with the house
            // Check if the player is outside or inside the house
            // If outside, go inside the house
            // If inside, go outside the house
            {
                if (Ext.activeSelf)
                {
                    Ext.gameObject.SetActive(false);
                    Int.gameObject.SetActive(true);
                }
                else
                {
                    bool isBabyExisting = GameObject.FindWithTag("Baby") != null;
                    if (isBabyExisting)
                    {
                        BabyScript babyScript = GameObject.FindWithTag("Baby").GetComponent<BabyScript>();
                        babyScript.StopBabyCry();
                        Ext.gameObject.SetActive(true);
                        Int.gameObject.SetActive(false);
                    }
                    else
                    {
                        Ext.gameObject.SetActive(true);
                        Int.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerInteract && FinishLevel.babyScript.isKidnapped)
        {
            FinishLevel.NextLevel();
        }
    }
}