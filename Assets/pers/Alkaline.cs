using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alkaline : MonoBehaviour
{
    public bool brewing = false, done = false, danger = false;
    public float duration = 3, unstable_duration = 3, damage = 30, dist = 2;
    public GameObject pers;

    public float timer = 0, unstable_timer = 0;
    private Controller contr;
    private Transform transf;
    private TextMeshPro text_1, text_2;
    private ParticleSystem particles;
    private GameObject timer_1, timer_2;

    void Start()
    {
        contr = pers.GetComponent<Controller>();
        transf = pers.transform;
        timer_1 = transform.GetChild(1).gameObject;
        timer_2 = transform.GetChild(2).gameObject;
        text_1 = transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        text_2 = transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>();
        particles = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            text_1.text = ((int)timer).ToString();
        }
        else if (brewing)
        {
            timer_1.SetActive(false);
            timer_2.SetActive(true);
            text_2.text = ((int)unstable_timer).ToString();
            
            brewing = false;
            done = true;
            danger = true;
            unstable_timer = unstable_duration;
        }

        if (unstable_timer > 0)
        {
            unstable_timer -= Time.deltaTime;
            text_2.text = ((int)unstable_timer).ToString();
        }
        else if (danger)
        {
            Bang();
        }

        if (!danger && !brewing)
        {
            timer_1.SetActive(false);
            timer_2.SetActive(false);
        }
    }

    public void Brew()
    {
        brewing = true;
        timer = duration;
        timer_1.SetActive(true);
        text_1.text = ((int)timer).ToString();
    }

    public void Bang()
    {
        float distance = Mathf.Pow(Mathf.Pow(transf.position.x - transform.position.x, 2)
            + Mathf.Pow(transf.position.y - transform.position.y, 2), 0.5f);
        if (danger)
        {
            if (!done || distance < dist)
                contr.hp -= damage;
            
            if (!done)
            {
                particles.gameObject.transform.parent = pers.transform;
                particles.gameObject.transform.localPosition = new Vector3(0.5f, 1, 0);
                particles.Play();
            }
            else
            {
                particles.gameObject.transform.parent = gameObject.transform;
                particles.gameObject.transform.localPosition = new Vector3(0.8f, 1.4f, 0);
                particles.Play();
            }
        }

        done = false;
        danger = false;
    }
}
