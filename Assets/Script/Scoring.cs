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
            FindObjectOfType<Collectables>().collectableSlider.value = Collectables.collectedCollectables; // Update the slider value
            AudioManager.Instance.playSFX("Collect Blood");
            if (LiveScript.Instance != null) LiveScript.Instance.UpdateUI();
        }
    }
}
