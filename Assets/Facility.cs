using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Facility : MonoBehaviour
{
    public int protons = 0, stable = 0;
    public float hot_delay = 3, explode_delay = 5, radius = 7, damage = 10, TT_1 = 60, TT_2 = 40;
    public GameObject screen, flash, cooler, pers;
    public bool mag_good = false, too_hot = false, RADIATION = false, finish = false;

    public int attempt = 0;
    public float hot_timer = 0, explode_timer = 10,
    // decay_time = 10000, decay_timer = 0,
    attempt_timer = 6, trouble_timer_1 = 0, trouble_timer_2 = 0;
    
    public GameObject rad, mag;
    private TextMeshPro text_mesh;
    private bool final_mode;

    void Start()
    {
        text_mesh = screen.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        mag = screen.transform.GetChild(1).gameObject;
        rad = screen.transform.GetChild(2).gameObject;
        Update_screen();
    }

    void Update()
    {
        if (flash.activeSelf == mag_good && mag.activeSelf)
        {
            mag.SetActive(false);
        }
        else if (flash.activeSelf != mag_good && !mag.activeSelf)
        {
            mag.SetActive(true);
        }

        if (hot_timer > 0)
            hot_timer -= Time.deltaTime;
        else if (too_hot)
        {
            RADIATION = true;
            rad.SetActive(true);
        }

        if (explode_timer > 0)
            explode_timer -= Time.deltaTime;
        else if (mag.activeSelf)
        {
            Bang();
            explode_timer = explode_delay;
        }


        if (protons > 149)
        {
            protons = stable;
            Update_screen();
            if (!too_hot)
                Too_hot();
            mag_good = !mag_good;
        }

        if (final_mode)
        {
            // if (decay_timer > 0)
            // {
            //     decay_timer -= Time.deltaTime;
            // }
            // else if (protons != stable)
            // {
            //     protons = stable;
            //     Update_screen();
            //     if (!too_hot)
            //         Too_hot();
            //     mag_good = !mag_good;
            // }

            if (attempt_timer > 0 && protons == 149)
            {
                attempt_timer -= Time.deltaTime;
            }
            else if (protons == 149 && (attempt < 4))
            {
                New_attempt();
                protons = stable;
                Update_screen();
                if (!too_hot)
                    Too_hot();
                mag_good = !mag_good;
            }
            else if (protons == 149)
            {
                finish = true;
            }

            if (trouble_timer_1 > 0)
            {
                trouble_timer_1 -= Time.deltaTime;
            }
            else
            {
                trouble_timer_1 = TT_1;
                if (!too_hot)
                    Too_hot();
            }

            if (trouble_timer_2 > 0)
            {
                trouble_timer_2 -= Time.deltaTime;
            }
            else
            {
                trouble_timer_2 = TT_2;
                mag_good = !mag_good;
            }
        }
    }

    public void Minus_rad()
    {
        RADIATION = false;
        too_hot = false;
        rad.SetActive(false);
    }

    public void Too_hot()
    {
        cooler.GetComponent<Cooler>().Turn_off();
        hot_timer = hot_delay;
        too_hot = true;
    }

    public void Enter(string element)
    {
        // if (protons == stable && final_mode)
        //     decay_timer = decay_time;

        switch (element)
        {
            case "Li":
                protons += 3;
                break;
            case "Na":
                protons += 11;
                break;
            case "K":
                protons += 19;
                break;
            case "La":
                protons += 57;
                break;
            case "U":
                protons += 92;
                break;
            case "Np":
                protons += 93;
                break;
            case "Pu":
                protons += 94;
                break;
        }
        Update_screen();
    }

    public void Update_screen()
    {
        if (protons == 149)
            text_mesh.text = protons.ToString();
        else
            text_mesh.text = protons.ToString() + "|" + (protons-149).ToString();
    }

    public void Bang()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        
        Transform transf = pers.transform;
        Vector3 center = new Vector3(transform.position.x + 6, transform.position.y + 1, 0);

        float distance = Mathf.Pow(Mathf.Pow(transf.position.x - center.x, 2)
            + Mathf.Pow(transf.position.y - center.y, 2), 0.5f);

        if (distance < radius)
        {
            pers.GetComponent<Controller>().hp -= damage;
        }
    }

    public void Final_mode()
    {
        final_mode = true;
    }

    public void New_attempt()
    {
        switch (attempt)
        {
            case 0:
                stable = 149 - 93;
                // decay_time = 60;
                attempt_timer = 3;
                break;
            case 1:
                stable = 149 - 94;
                // decay_time = 60;
                attempt_timer = 3;
                break;
            case 2:
                stable = 149 - 95;
                // decay_time = 60;
                attempt_timer = 3;
                break;
            case 3:
                stable = 149 - 101;
                // decay_time = 20;
                attempt_timer = 9;
                break;
            case 4:
                stable = 149;
                // decay_time = 20000;
                attempt_timer = 10;
                break;
        }
        attempt++;
    }
}
