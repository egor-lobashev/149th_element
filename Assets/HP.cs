using UnityEngine;

public class HP : MonoBehaviour
{
    private GameObject player;
    private Controller health;
    private Transform transform_1;
    private float HP_max;

    void Start()
    {
        player = transform.parent.parent.gameObject;
        health = player.GetComponent<Controller>();
        HP_max = health.HP_max;
        transform_1 = GetComponent<Transform>();
    }

    void Update()
    {
        float HP = health.hp;
        HP = HP > 0 ? HP : 0;
        transform_1.localScale = new Vector3(HP/HP_max, 1, 1);
    }
}