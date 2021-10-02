using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed, slow = 0.03f, fast = 0.08f;
    public Vector2 dr;
    
    public bool amogus = false;

    private float last_clothes = 0;

    void FixedUpdate()
    {
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        dr = new Vector2(move_x, move_y).normalized;
        dr *= speed;

        gameObject.transform.position += (Vector3)dr;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        string place = other.gameObject.name;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Hello");
            switch (place)
            {
                case "clothes":
                    if (Time.time - last_clothes >= 0.2f)
                    {
                        amogus = !amogus;
                        speed = amogus ? slow : fast;
                        last_clothes = Time.time;
                    }
                    break;
            }
        }
    }
}
