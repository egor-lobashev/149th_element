using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    public GameObject lamp;
    public bool hot = false;

    private SpriteRenderer cold_sprite;

    void Start()
    {
        cold_sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Turn_on()
    {
        hot = false;
        cold_sprite.enabled = true;
        lamp.SetActive(false);
        transform.parent.gameObject.GetComponent<Facility>().Minus_rad();
    }

    public void Turn_off()
    {
        hot = true;
        cold_sprite.enabled = false;
        lamp.SetActive(true);
    }
}
