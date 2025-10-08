using UnityEngine;

// This script controls the behavior of a door (window).
public class Door : MonoBehaviour
{
    public Collider2D colliderToDeactivate;
    public GameObject Open, Close;

    bool playerInteract;
    void Start()
    {
        colliderToDeactivate = GetComponent<Collider2D>();
    }


    void Update()
    {
        PlayerInteracted();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
        if (other.CompareTag("NPC"))
        {
            colliderToDeactivate.enabled = false;
            Close.SetActive(false);
            Open.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteract = true;
        }
        if (other.CompareTag("NPC"))
        {
            colliderToDeactivate.enabled = false;
            Close.SetActive(false);
            Open.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            colliderToDeactivate.enabled = true;
            Close.SetActive(true);
            Open.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            playerInteract = false;
        }
    }

    void PlayerInteracted()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInteract)
        {
            if (colliderToDeactivate.isActiveAndEnabled)
            {
                colliderToDeactivate.enabled = false;
                Close.SetActive(false);
                Open.SetActive(true);
            }
            else
            {
                colliderToDeactivate.enabled = true;
                Close.SetActive(true);
                Open.SetActive(false);
            }
        }
    }
}
