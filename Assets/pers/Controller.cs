using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string holding = "";
    public float speed, slow = 0.03f, fast = 0.08f, hp = 100, HP_max = 100, rad_delay = 0.3f, rad_dmg = 5;
    public Vector2 dr;
    
    public bool amogus = false;

    public GameObject Na, K, Li, facility_go;

    private float last_clothes = 0, last_lever = 0, rad_timer = 0;
    public Facility facility;
    public GameObject place;

    void Start()
    {
        place = gameObject;
        facility = facility_go.GetComponent<Facility>();
    }

    void FixedUpdate()
    {
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        dr = new Vector2(move_x, move_y).normalized;
        dr *= speed;

        gameObject.transform.position += (Vector3)dr;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        place = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        place = gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (place.name)
            {
                case "clothes":
                    if (Time.time - last_clothes >= 0.2f)
                    {
                        amogus = !amogus;
                        speed = amogus ? slow : fast;
                        last_clothes = Time.time;
                    }
                    break;

                case "lever":
                    if ((Time.time - last_lever >= 0.2f) &&
                        (gameObject.GetComponent<My_animator>().direction == 3))
                    {
                        place.GetComponent<Magnetic>().Switch();
                        last_lever = Time.time;
                    }
                    break;

                case "U":
                case "Pu":
                case "Np":
                case "H2O":
                    if ((holding != place.name) &&
                            (gameObject.GetComponent<My_animator>().direction == 1))
                        holding = place.name;
                    break;
                case "La":
                    if ((holding != place.name) &&
                            ((gameObject.GetComponent<My_animator>().direction == 1) ||
                             (gameObject.GetComponent<My_animator>().direction == 2)))
                        holding = place.name;
                    break;

                case "Li":
                case "Na":
                case "K":
                    if (gameObject.GetComponent<My_animator>().direction == 1)
                    {
                        Alkaline alk = place.GetComponent<Alkaline>();
                        
                        if (!alk.brewing && !alk.done)
                        {
                            alk.Brew();
                        }
                        else if (alk.done)
                        {
                            holding = place.name;
                            alk.done = false;
                        }
                    }
                    break;

                case "A":
                    if ((holding == "U" || holding == "Np" || holding == "Pu") &&
                        (gameObject.GetComponent<My_animator>().direction == 2))
                    {
                        place.transform.parent.gameObject.GetComponent<Facility>().Enter(holding);
                        holding = "";
                    }
                    break;

                case "B":
                    if ((holding == "Li" || holding == "Na" || holding == "K" || holding == "La") &&
                        (gameObject.GetComponent<My_animator>().direction == 3))
                    {
                        place.transform.parent.gameObject.GetComponent<Facility>().Enter(holding);
                        switch (holding)
                        {
                            case "Na":
                                Na.transform.GetChild(0).gameObject.GetComponent<Alkaline>().danger = false;
                                break;
                            case "Li":
                                Li.transform.GetChild(0).gameObject.GetComponent<Alkaline>().danger = false;
                                break;
                            case "K":
                                K.transform.GetChild(0).gameObject.GetComponent<Alkaline>().danger = false;
                                break;
                        }
                        holding = "";
                    }
                    break;
                
                case "cooler":
                    if (place.GetComponent<Cooler>().hot && holding == "H2O")
                    {
                        holding = "";
                        place.GetComponent<Cooler>().Turn_on();
                    }
                    break;
            }
        }

        if (rad_timer > 0)
            rad_timer -= Time.deltaTime;
        else if ((facility.RADIATION || holding == "U" || holding == "Np"|| holding == "Pu" ||
            transform.position.x < -16.6f) && !amogus)
        {
            hp -= rad_dmg;
            rad_timer = rad_delay;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
