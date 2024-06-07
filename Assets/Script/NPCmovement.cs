using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    NPCBehaviour npcBehaviour;
    Animator animator;
    public Transform[] waypoints; // Array untuk menyimpan titik-titik tujuan NPC
    public int currentWaypoint = 0; // Indeks titik tujuan saat ini
    private float idleTimer = 0f; // Penghitung waktu untuk perilaku idle
    private float idleDuration = 5f; // Durasi idle dalam detik

    private float speed = 2f; // Kecepatan NPC
    public bool isIdle = true; // Status apakah NPC sedang idle atau tidak

    private void Start()
    {
        animator = GetComponent<Animator>();
        npcBehaviour = GetComponent<NPCBehaviour>();
    }

    void Update()
    {
        Animator();
        if (!npcBehaviour.isSuspicious)
        {
            patrolling();
        }
    }

    void patrolling()
    {
        // Jika NPC sedang idle
        if (isIdle)
        {

            idleTimer += Time.deltaTime; // Tambahkan waktu ke penghitung idle

            // Jika durasi idle telah tercapai
            if (idleTimer >= idleDuration)
            {
                isIdle = false; // Atur status NPC menjadi tidak idle
                idleTimer = 0f; // Reset penghitung idle
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (!isIdle) // Jika NPC tidak idle
        {
            // Dapatkan posisi titik tujuan saat ini
            Vector2 targetPosition = waypoints[currentWaypoint].position;

            // Hitung arah pergerakan NPC
            Vector2 direction = targetPosition - (Vector2)transform.position;

            // Ubah arah menghadap NPC berdasarkan arah pergerakan
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;// Menghadap ke kanan
            }
            else if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;// Menghadap ke kiri
            }

            // Gerakkan NPC menuju titik tujuan
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Jika NPC telah mencapai titik tujuan
            if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
            {
                isIdle = true; // Atur status NPC menjadi idle
                currentWaypoint++; // Pindah ke titik tujuan berikutnya

                // Jika telah mencapai titik tujuan terakhir, kembali ke titik awal
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
        }
    }
    void Animator()
    {
        // Jika NPC sedang idle
        if (isIdle && !npcBehaviour.isSuspicious)
        {
            animator.SetBool("isIdle", true);

        }
        else // Jika NPC tidak idle (sedang berjalan)
        {
            animator.SetBool("isIdle", false);
        }

    }

}