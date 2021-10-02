using UnityEngine;

public class Pers_animator : MonoBehaviour
{
    public float walk_frame_duration = 0.2f;
    private My_animator my_animator;
    private Controller controller;

    void Start()
    {
        controller = gameObject.GetComponent<Controller>();
        my_animator = gameObject.GetComponent<My_animator>();
    }

    void FixedUpdate()
    {
        string new_action;
        int new_direction = my_animator.direction;
        Vector2 dr = controller.dr;

        if (dr[0] == 0 && dr[1] == 0)
        {
            new_action = "idle";
        }
        else
        {
            new_action = "walk";

            if (dr[1] > 0 && dr[0] == 0)
            {
                new_direction = 1;
            }
            else if (dr[1] >= 0)
            {
                new_direction = (dr[0] >= 0 ? 2 : 3);
            }
            else
            {
                new_direction = 0;
            }
        }
        if (controller.amogus)
        {
            new_action += "_amogus";
        }

        if (new_action != my_animator.action || new_direction != my_animator.direction)
        {
            int frames = 2;
            float frame_duration = 1;

            switch (new_action)
            {
                case "walk":
                case "walk_amogus":
                    frames = 4;
                    frame_duration = walk_frame_duration;
                    break;
                case "idle":
                case "idle_amogus":
                    frames = 1;
                    frame_duration = 10;
                    break;
            }
            my_animator.Change_animation(new_action, new_direction, frames, frame_duration);
        }
    }
}
