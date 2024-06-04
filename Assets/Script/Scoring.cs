using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
            Collectables.collectedCollectables++; // Increase the number of collected collectables
            Collectables collectables = FindObjectOfType<Collectables>(); // Obtain a reference to the Collectables object
            collectables.GetComponent<Slider>().value = Collectables.collectedCollectables; // Update the slider value
        }
    }
}
