using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby_script : MonoBehaviour
{
    public bool isSleeping = true; // apakah bayi sedang tidur atau tidak
    public float minSleepDuration = 5f; // Durasi tidur minimum dalam detik
    public float maxSleepDuration = 10f; // Durasi tidur maksimum dalam detik
    private float sleepTimer = 0f; // Penghitung waktu untuk perilaku tidur
                                   // NPC yang akan dipanggil ketika bayi menangis


}

