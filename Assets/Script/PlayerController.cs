using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float speed = 100.0f;
    
    //Camera's zoom needed!
    private float zoom;
    private float zoomMultiplier = 4f;
    private float zoomMin = 1f;
    private float zoomMax = 10f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set the initial zoom of the camera
        zoom = Camera.main.orthographicSize;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        movement();
        cameraZoom();
        
    }

    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            if (h > 0)
            {
                transform.localScale = new Vector3(0.1f, 0.1f, 1);
            }
            else if (h < 0)
            {
                transform.localScale = new Vector3(-0.1f, 0.1f, 1);
            }
        }
        rb.velocity = new Vector2(h, v) * speed * Time.deltaTime;
    }

    /// Zooms in the camera by setting the orthographic size of the main camera to the current value of the zoom variable.
    void cameraZoom()
    {   

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll*zoomMultiplier;
        zoom = Mathf.Clamp(zoom,zoomMin,zoomMax);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize,zoom,ref velocity,smoothTime);
    }
}
