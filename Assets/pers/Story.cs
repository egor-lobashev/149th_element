using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public int stage = 0;
    
    private TextMeshPro prof_text;
    private Controller controller;
    private GameObject speak;
    private bool damaged = false, crazy = false;
    private float timer = 0;

    void Start()
    {
        prof_text = transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshPro>();
        controller = gameObject.GetComponent<Controller>();
        speak = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        switch (stage)
        {                       
// good morning! i have some good news. you are very lucky to be my phd student, because today we will do something unusual
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    prof_text.text =
"while all other scientists are trying to synthesize 119th chemical element, we'll try to get 149th one";
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    prof_text.text =
"i suggest to call it after us, such a beautiful word:  \n\n   ludumdarium\n\n  or just ld-149";
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    prof_text.text =
"so, let's get to the point. we need to receive exactly 149 protons. you can se at the screen current amount of them";
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    prof_text.text =
"first, take some lantanium from the case\n[press e to interact]";
                }
                break;

            case 4:
            case 6:
            case 11:
            case 14:
            case 16:
            case 18:
            case 21:
            case 29:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(false);
                }
                break;

            case 5:
                if (controller.holding == "La")
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"good! now you should put it to the\nport 'b'\n[press e to put]";
                }
                break;
            
            case 7:
                if (controller.facility.protons == 57)
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"well done! look at the screen: you see that now we have 57 protons - it is the number of lantanium!";
                }
                break;
            
            case 8:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"if you forget the number of some element, you always can check the periodic table\n[press c to open/close the periodic table]";
                }
                break;
            
            case 9:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"(the person who discovered the new element don't remember the periodic system, ha-ha)";
                }
                break;
            
            case 10:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"so, what did i talk about? ah, yes. now go to the radioactive room, take some plutonium and put it to the port A";
                }
                break;
            
            case 12:
                if (transform.position.x < -16.6 && !controller.amogus)
                {
                    damaged = true;
                    stage--;
                    speak.SetActive(true);
                    prof_text.text =
"stop! go back!!!\n\n\n[press D or right arrow]";
                }
                if (transform.position.x > -16.1 && damaged && !crazy)
                {
                    crazy = true;
                    stage--;
                    speak.SetActive(true);
                    prof_text.text =
"are you crazy? put on the protective suit first";
                }
                if (controller.amogus)
                    stage++;
                break;

            case 13:
                if (controller.holding == "Pu" && controller.amogus)
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"(57 + 94 = 151, hell, my mistake)\n\nha, you've taken plutonium! i just wanted to check your attention. actually we need uranium.\n57 + 92 = 149, right?";
                }
                break;
            
            case 15:
                if (controller.holding == "U")
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"ok, put it to the a port. only after that you'll be able to take off the suit";
                }
                break;

            case 17:
                if (controller.facility.protons == 149)
                {
                    stage++;
                    speak.SetActive(true);
                    timer = 9;
                    prof_text.text =
"hurrah! now we need to wait for 10 seconds. only after that we will oficially become the explorers of the new element!";
                }
                break;
            
            case 19:
                if (timer > 0)
                    timer -= Time.deltaTime;
                else
                {
                    stage++;
                    speak.SetActive(true);
                    controller.facility.protons = 138;
                    controller.facility.stable = 138;
                    controller.facility.Update_screen();
                    prof_text.text =
"oh, no! it's unstable!\nit has lost 11 protons. go to the table with the bowl of salt and make some sodium";
                }
                break;

            case 20:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"but it is unstable too. few seconds after the reaction it will react with water making an explosion";
                }
                break;
            
            case 22:
                if (controller.facility.protons == 149)
                {
                    stage++;
                    speak.SetActive(true);
                    controller.facility.protons = 130;
                    controller.facility.stable = 130;
                    controller.facility.Update_screen();
                    prof_text.text =
"turn the lever to turn on the magnetic field. maybe it will help to stabilize the ld-149";
                }
                break;

            case 23:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(false);
                    controller.facility.mag_good = true;
                }
                break;
            
            case 24:
                if (!controller.facility.mag.activeSelf)
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"i hope you've understood the task. but there are some more problems.";
                }
                break;

            case 25:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"first, you have to switch the lever every time when the magnetic field icon appears at the screen. otherwise the facility will make explosions";
                }
                break;

            case 26:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"second, you need to bring water to the refregerant, which is under the port a, when it is too hot";
                }
                break;
            
            case 27:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    prof_text.text =
"otherwise radiation will come from the facility. you can put on a protection suit to avoid it.";
                }
                break;
            
            case 28:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stage++;
                    speak.SetActive(true);
                    controller.facility.Final_mode();
                    prof_text.text =
"the tables at the right are:\n\nK\nNa (you know it)\nLi\n\nso, good luck, mr. Dare!";
                }
                break;
            
            case 30:
                if (controller.facility.finish)
                {
                    speak.SetActive(true);
                    prof_text.text =
"hurrah, we did it! now ludium...  ludumium...  ludumdarium\n\nis discovered!";
                }
                break;
        }

        if (controller.hp <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
