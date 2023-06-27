using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    private Vector2 angle = new Vector2(180 * Mathf.Deg2Rad, 0 * Mathf.Deg2Rad);

    public Transform follow;
    public float maxDistance = 3;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            angle.x += hor * Mathf.Deg2Rad;
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            angle.y += ver * Mathf.Deg2Rad;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }
    }

    void LateUpdate()
    {
        Vector3 orbit = new Vector3(
            Mathf.Sin(angle.x) * Mathf.Cos(angle.y),
            -Mathf.Sin(angle.y),
            Mathf.Cos(angle.x) * Mathf.Cos(angle.y));

        float distance = maxDistance;

        transform.position = follow.position + orbit * distance;
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }
}
