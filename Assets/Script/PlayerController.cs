using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float speed = 100.0f;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pause();
    }
    void FixedUpdate()
    {
        movement();
        
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

    void pause(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Time.timeScale == 0){
                Time.timeScale = 1;
            }
            else{
                Time.timeScale = 0;
            }
        }
    }

}
