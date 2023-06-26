using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCArrow : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
