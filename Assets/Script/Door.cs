using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Collider2D colliderToDeactivate;
    public GameObject Open, Close;
    void Start()
    {
        colliderToDeactivate = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            colliderToDeactivate.enabled = false;
            Close.SetActive(false);
            Open.SetActive(true);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
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
        if (other.tag == "NPC")
        {
            colliderToDeactivate.enabled = false;
            Close.SetActive(false);
            Open.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            colliderToDeactivate.enabled = true;
            Close.SetActive(true);
            Open.SetActive(false);
        }

    }
}



