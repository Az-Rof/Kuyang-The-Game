using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum NPCState
{
    Patrolling,
    InvestigatingBaby,
    SuspiciousOfPlayer
}

public class NPCBehaviour : MonoBehaviour
{
    public float suspicionLevel;
    public bool isSuspicious;
    private bool wasSuspicious = false;
    public float suspicionThreshold = 3f;
    public BoxCollider2D suspicionZone;
    Vector2 lastKnownPlayerPosition;
    public float speed = 3.0f;

    public Transform[] waypoints; // Array untuk menyimpan titik-titik tujuan NPC
    public int currentWaypoint = 0; // Indeks titik tujuan saat ini
    private float idleTimer = 0f; // Penghitung waktu untuk perilaku idle
    private float idleDuration = 3f; // Durasi idle dalam detik
    public bool isIdle = true; // Status apakah NPC sedang idle atau tidak

    [Tooltip("Jika true, NPC akan mendatangi bayi yang menangis.")]
    public bool shouldInvestigateBaby = true;

    // Keputusan untuk memakai tangga/lift ChangeFloor
    public bool wantChangeLevel;
    public bool ischanginglevel;
    public float cooldown = 0;

    // --- Cached References ---
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private GameObject playerObject;
    private PlayerController playerController;

    private GameObject babyObject;
    private BabyScript babyScript;

    private List<ChangeFloor> changeFloors;

    // --- State Machine ---
    private NPCState currentState;

    void Start()
    {
        // Cache components on this GameObject
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Cache scene objects
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        babyObject = GameObject.FindGameObjectWithTag("Baby");
        if (babyObject != null)
        {
            babyScript = babyObject.GetComponent<BabyScript>();
        }

        // Cache all ChangeFloor objects and sort them by floor level
        changeFloors = new List<ChangeFloor>(FindObjectsOfType<ChangeFloor>());
        changeFloors.Sort((a, b) => a.connectFloor.transform.position.y.CompareTo(b.connectFloor.transform.position.y));
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        UpdateSuspicion();
        DetermineState();
        ExecuteState();
        UpdateAnimator();
    }

    private void DetermineState()
    {
        if (isSuspicious)
        {
            currentState = NPCState.SuspiciousOfPlayer;
        }
        else if (shouldInvestigateBaby && babyObject != null && !babyScript.isSleeping)
        {
            currentState = NPCState.InvestigatingBaby;
        }
        else
        {
            currentState = NPCState.Patrolling;
        }
    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case NPCState.SuspiciousOfPlayer:
                HandleSuspiciousState();
                break;
            case NPCState.InvestigatingBaby:
                HandleInvestigatingBabyState();
                break;
            case NPCState.Patrolling:
                HandlePatrollingState();
                break;
        }

        if (wantChangeLevel)
        {
            HandleChangeLevel();
        }
    }

    private void HandleInvestigatingBabyState()
    {
        if (babyObject == null || !shouldInvestigateBaby) return;

        Vector2 babyPosition = babyObject.transform.position;
        float yDifference = Mathf.Abs(transform.position.y - babyPosition.y);

        if (yDifference >= 3f)
        {
            wantChangeLevel = true;
        }
        else
        {
            wantChangeLevel = false;
            MoveTowards(babyPosition);

            if (Mathf.Approximately(transform.position.x, babyPosition.x))
            {
                isIdle = true;
            }
            else
            {
                isIdle = false;
            }
        }
    }

    private void UpdateSuspicion()
    {
        if (playerObject != null && playerController != null)
        {
            // Meningkatkan tingkat kecurigaan jika pemain berada di SusZone
            if (suspicionZone.bounds.Contains(playerObject.transform.position)
            && !playerController.isInvisible && !playerController.isHiding)
            {
                suspicionLevel += Time.deltaTime;
                lastKnownPlayerPosition = playerObject.transform.position;
            }
            else
            {
                // Mengurangi tingkat kecurigaan jika pemain tidak berada di SusZone
                suspicionLevel -= Time.deltaTime;
            }

            // Memastikan tingkat kecurigaan berada di antara 0 dan suspicionThreshold
            suspicionLevel = Mathf.Clamp(suspicionLevel, 0, suspicionThreshold);

            isSuspicious = suspicionLevel >= suspicionThreshold;
        }
        wasSuspicious = isSuspicious;
    }

    private void HandleSuspiciousState()
    {
        Vector2 direction = new Vector2(lastKnownPlayerPosition.x - transform.position.x, 0).normalized;
        rb.velocity = direction * speed * 1.15f;
        FlipSprite(direction.x);
        wantChangeLevel = false; // Player chase overrides changing level for other reasons
    }

    private void HandlePatrollingState()
    {
        rb.velocity = Vector2.zero;
        if (wasSuspicious)
        {
            currentWaypoint = UnityEngine.Random.Range(0, waypoints.Length);
        }

        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                isIdle = false;
                idleTimer = 0f;
            }
            FlipSprite(1); // Face default direction
        }
        else
        {
            Vector2 targetPosition = waypoints[currentWaypoint].position;
            float yDifference = Mathf.Abs(transform.position.y - targetPosition.y);

            if (yDifference >= 2f)
            {
                wantChangeLevel = true;
            }
            else
            {
                wantChangeLevel = false;
                transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, targetPosition.x, speed * Time.deltaTime), transform.position.y);
                Vector2 direction = targetPosition - (Vector2)transform.position;
                FlipSprite(direction.x);
            }

            if (Vector2.Distance(transform.position, targetPosition) < 1f)
            {
                isIdle = true;
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
        }
    }

    private void HandleChangeLevel()
    {
        float targetY = (currentState == NPCState.InvestigatingBaby && shouldInvestigateBaby && babyObject != null)
            ? babyObject.transform.position.y
            : waypoints[currentWaypoint].transform.position.y;

        ChangeFloor nearestChangeFloor = FindNearestChangeFloor(targetY);

        if (nearestChangeFloor != null)
        {
            Vector2 direction = new Vector2(nearestChangeFloor.transform.position.x - transform.position.x, 0).normalized;
            rb.velocity = direction * speed;
            FlipSprite(direction.x);
            if (Mathf.Abs(nearestChangeFloor.transform.position.x - transform.position.x) <= 2f)
            {
                ischanginglevel = true;
            }
        }
        else
        {
            // Fallback: move towards target on X axis if no valid floor change is found
            float targetX = (currentState == NPCState.InvestigatingBaby && shouldInvestigateBaby && babyObject != null)
                ? babyObject.transform.position.x
                : waypoints[currentWaypoint].transform.position.x;

            Vector2 direction = new Vector2(targetX - transform.position.x, 0).normalized;
            rb.velocity = direction * speed;
            FlipSprite(direction.x);
        }
    }

    private ChangeFloor FindNearestChangeFloor(float targetY)
    {
        ChangeFloor nearestChangeFloor = null;
        float nearestDistance = float.MaxValue;

        foreach (ChangeFloor changeFloor in changeFloors)
        {
            float distance = Mathf.Abs(changeFloor.transform.position.y - transform.position.y);
            bool isSameDirection = (targetY - transform.position.y) * (changeFloor.connectFloor.transform.position.y - transform.position.y) > 0;

            // Check if this floor change is closer and goes in the right direction.
            if (distance < nearestDistance && isSameDirection)
            {
                nearestChangeFloor = changeFloor;
                nearestDistance = distance;
            }
        }
        return nearestChangeFloor;
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, transform.position.y), speed * Time.deltaTime);
        Vector2 direction = targetPosition - (Vector2)transform.position;
        FlipSprite(direction.x);
    }

    private void FlipSprite(float directionX)
    {
        if (directionX > 0.01f)
        {
            spriteRenderer.flipX = false; // Menghadap ke kanan
        }
        else if (directionX < -0.01f)
        {
            spriteRenderer.flipX = true; // Menghadap ke kiri
        }
    }

    private void UpdateAnimator()
    {
        if (isIdle && !isSuspicious)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }
}
