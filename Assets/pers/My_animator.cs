using UnityEngine;

public class My_animator : MonoBehaviour
{
    public UnityEngine.U2D.SpriteAtlas atlas;
    public string action = "*not set*";
    public int direction = 0;
    public int frames = 2;
    public float frame_duration = 0.2f;
    
    private int frame = 0;
    private float timer = 0;
    private SpriteRenderer sprite_renderer;

    void Start()
    {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= frame_duration)
        {
            timer = 0;
            frame = (frame + 1) % frames;
            sprite_renderer.sprite = atlas.GetSprite(action + "_" + (frame + direction*frames).ToString());
        }
    }

    public void Change_animation(string act, int dir, int frm, float frm_durarion)
    {
        action = act;
        direction = dir;
        
        frames = frm;
        frame_duration = frm_durarion;

        timer = frame_duration;
        frame = frames - 1;
    }
}
