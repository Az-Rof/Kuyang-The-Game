using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;



public class ChangeFloor : MonoBehaviour
{
    public GameObject connectFloor;
    public Vector2 newFloorPosition;
    void Start()
    {
        newFloorPosition = connectFloor.transform.position;
    }

    void Update()
    {
        changefloor();
    }

    void changefloor()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            other.transform.position = newFloorPosition;
        }
    }
}
