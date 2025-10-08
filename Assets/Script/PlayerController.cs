using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    float speed = 150f; // Player's movement speed

    //Camera's zoom needed!
    private float zoom;
    private float zoomMultiplier = 4f;
    private float zoomMin = 1f;
    private float zoomMax = 10f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;


    // Camera's movement
    [SerializeField] private float maxCameraOffset = 100;
    [SerializeField] private float cameraReturnSpeed = 5f;
    private Vector3 cameraInitialLocalPos;
    private Vector3 cameraDragOffset;
    private bool isDraggingCamera = false;


    [SerializeField] GameObject invisible;
    public bool isInvisible;

    public bool isHiding;

    // Referensi ke tempat persembunyian yang sedang aktif
    private HideScript currentHidingSpot;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set the initial zoom of the camera
        zoom = Camera.main.orthographicSize;
        isInvisible = false;
        isHiding = false;
        AudioManager.Instance.playMusic("MainMenu Music");

        // Simpan posisi awal kamera relatif ke player
        cameraInitialLocalPos = Camera.main.transform.localPosition;
    }


    private void Update()
    {
        beinvisible();
        HandleHidingInput();
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        movement();
        cameraZoom();
        behiding();
        HandleCameraDrag();
    }


    void movement()
    {
        rb.velocity = moveInput * speed;
        if (!isHiding)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Jika player bergerak, kamera kembali ke posisi awal
            if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
            {
                cameraDragOffset = Vector3.Lerp(cameraDragOffset, Vector3.zero, Time.deltaTime * cameraReturnSpeed);
            }

            if (h != 0)
            {
                if (h > 0)
                {
                    transform.localScale = new Vector3(0.1f, 0.1f, 1);
                    invisible.transform.localScale = new Vector3(10, 10, 0);
                }
                else if (h < 0)
                {
                    transform.localScale = new Vector3(-0.1f, 0.1f, 1);
                    invisible.transform.localScale = new Vector3(10, 10, 0);
                }
            }
            rb.velocity = new Vector2(h, v) * speed * Time.deltaTime;
        }
    }


    /// Zooms in the camera by setting the orthographic size of the main camera to the current value of the zoom variable.
    void cameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref velocity, smoothTime);
    }
    // Move camerra based on input of mouse click and drag
    void beinvisible()
    {
        if (((float)Collectables.collectedCollectables / Collectables.totalCollectables) >= 0.79f)
        {
            isInvisible = true;
            //set player layer to other layer
            gameObject.layer = 10;
            invisible.SetActive(true);
        }
        else
        {
            isInvisible = false;
            invisible.SetActive(false);
        }
    }
    void behiding()
    {
        if (isHiding)
        {
            // Make player invisible
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            // Make player visible
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    /// Controls the camera movement based on mouse input.
    /// Move camera based on input of mouse click and drag when there is no input from the player
    void HandleCameraDrag()
    {
        Camera cam = Camera.main;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isDraggingCamera = true;

            // Arah drag berdasarkan gerakan mouse
            Vector3 mouseDelta = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0);
            cameraDragOffset += mouseDelta * 10f; // kecepatan drag (atur sesuai kenyamanan)

            // Batasi jarak maksimum offset
            cameraDragOffset = Vector3.ClampMagnitude(cameraDragOffset, maxCameraOffset);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraggingCamera = false;
        }

        // Update posisi kamera lokal (offset dari posisi awal)
        cam.transform.localPosition = cameraInitialLocalPos + cameraDragOffset;
    }
    public void ToggleHide()
    {
        isHiding = !isHiding;
    }

    private void HandleHidingInput()
    {
        // Jika pemain menekan 'E' dan berada di tempat persembunyian, atau sudah bersembunyi
        if (Input.GetKeyDown(KeyCode.E) && (currentHidingSpot != null || isHiding))
        {
            currentHidingSpot?.ToggleHide();
        }
    }

    // Metode ini akan dipanggil oleh HideScript
    public void SetHidingSpot(HideScript hidingSpot)
    {
        currentHidingSpot = hidingSpot;
    }

    public void ClearHidingSpot(HideScript hidingSpot)
    {
        if (currentHidingSpot == hidingSpot) currentHidingSpot = null;
    }

    #region  PlayerInputHandler
    // Mobile GUI
    private Vector2 moveInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnHide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Hide pressed!");
        }
    }
    #endregion
}
