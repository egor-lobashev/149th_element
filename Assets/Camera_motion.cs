using UnityEngine;

public class Camera_motion : MonoBehaviour
{
    public GameObject pers;
    public float shift_up;
    private float pos_z;

    void Start()
    {
        pos_z = transform.position[2];
    }

    void Update()
    {
        transform.position = new Vector3(pers.transform.position[0], pers.transform.position[1] + shift_up, pos_z);
    }
}
