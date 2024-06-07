using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public float suspicionLevel;
    public bool isSuspicious;
    private bool wasSuspicious = false;
    public float suspicionThreshold = 3f;
    public BoxCollider2D suspicionZone;
    // [SerializeField] GameObject player;
    Vector2 lastKnownPlayerPosition;
    public float speed = 2.0f;

    void Start()
    {

    }


    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Meningkatkan tingkat kecurigaan jika pemain berada di SusZone
            if (suspicionZone.bounds.Contains(player.transform.position) && !player.GetComponent<PlayerController>().isInvisible)
            {
                suspicionLevel += Time.deltaTime;
                lastKnownPlayerPosition = player.transform.position;
            }
            else
            {
                // Mengurangi tingkat kecurigaan jika pemain tidak berada di SusZone
                suspicionLevel -= Time.deltaTime;
            }

            // Memastikan tingkat kecurigaan berada di antara 0 dan suspicionThreshold
            suspicionLevel = Mathf.Clamp(suspicionLevel, 0, suspicionThreshold);

            // NPC menjadi curiga jika tingkat kecurigaan mencapai ambang batas
            isSuspicious = suspicionLevel >= suspicionThreshold;

            // Jika NPC curiga, bergerak ke posisi terakhir player
            if (isSuspicious)
            {
                Vector2 direction = new Vector2(lastKnownPlayerPosition.x - transform.position.x, 0).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
                if (direction.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (direction.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (wasSuspicious)
                {
                    GetComponent<NPCMovement>().currentWaypoint = UnityEngine.Random.Range(0, GetComponent<NPCMovement>().waypoints.Length);
                }
            }
        }

        wasSuspicious = isSuspicious;
    }
}