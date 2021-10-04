using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public GameObject flash;
    private SpriteRenderer on_sprite, off_sprite;

    void Start()
    {
        on_sprite = gameObject.GetComponent<SpriteRenderer>();
        off_sprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    public void Switch()
    {
        if (on_sprite.enabled)
            Turn_off();
        else
            Turn_on();        
    }

    public void Turn_on()
    {
        on_sprite.enabled = true;
        off_sprite.enabled = false;
        flash.SetActive(true);
    }

    public void Turn_off()
    {
        on_sprite.enabled = false;
        off_sprite.enabled = true;
        flash.SetActive(false);
    }
}
