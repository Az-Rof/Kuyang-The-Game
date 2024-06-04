using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; // Import this to use UI elements

public class Collectables : MonoBehaviour
{

    public Slider collectableSlider; // Reference to the slider UI
    public static int totalCollectables; // Total number of collectables
    public static int collectedCollectables; // Number of collected collectables

    private void Start()
    {
        totalCollectables = GameObject.FindGameObjectsWithTag("Collectables.Blood").Count();//GameObject.FindGameObjectsWithTag("Collectable").Where(obj => obj.name == "Blood").Count();
        collectedCollectables = 0;
        collectableSlider.maxValue = totalCollectables; // Set the max value of the slider
        collectableSlider.value = collectedCollectables; // Set the current value of the slider
    }

}