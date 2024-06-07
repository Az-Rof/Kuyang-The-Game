using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
using UnityEngine.VFX;


public class PlayerController : MonoBehaviour
{
    //public string[] interactobjek;    , IPointerEnterHandler, IPointerExitHandler
    Rigidbody2D rb;

    // GameObject selectedObject;
    public float speed = 100.0f;

    //Camera's zoom needed!
    private float zoom;
    private float zoomMultiplier = 4f;
    private float zoomMin = 1f;
    private float zoomMax = 10f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField] GameObject invisible;

    public bool isInvisible;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set the initial zoom of the camera
        zoom = Camera.main.orthographicSize;
        isInvisible = false;
    }


    private void Update()
    {
        beinvisible();
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        movement();
        cameraZoom();


        // OnPointerClicked();
    }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     if (selectedObject != null)
    //     {
    //         Light2D light2D = selectedObject.GetComponent<ShineOnApproach>().GetComponent<Light2D>();
    //         if (light2D != null)
    //         {
    //             light2D.enabled = true;
    //         }
    //     }
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if (selectedObject != null)
    //     {
    //         Light2D light2D = selectedObject.GetComponent<ShineOnApproach>().GetComponent<Light2D>();
    //         if (light2D != null)
    //         {
    //             light2D.enabled = false;
    //         }
    //     }
    // }


    // void OnPointerClicked()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //         if (hit)
    //         {
    //             selectedObject = hit.transform.gameObject;
    //             InteractWithObject(selectedObject);
    //         }
    //         else
    //         {
    //             selectedObject = null;
    //         }

    //     }
    //     else
    //     {

    //     }
    // }

    // private void InteractWithObject(GameObject objectToInteract)
    // {
    //     // Add your interaction logic here
    //     Debug.Log("Interacting with " + objectToInteract.name);
    // }


    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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
            // AudioManager.Instance.playSFX("Fly");
        }
        else
        {
            // AudioManager.Instance.stopSFX("Fly");
        }
        rb.velocity = new Vector2(h, v) * speed * Time.deltaTime;

    }

    /// Zooms in the camera by setting the orthographic size of the main camera to the current value of the zoom variable.
    void cameraZoom()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref velocity, smoothTime);
    }
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
}
