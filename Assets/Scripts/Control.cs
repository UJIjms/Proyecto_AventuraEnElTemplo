using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private int times = 0;
    public GameObject text;
    public GameObject back;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && times == 0)
        {
            text.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            times++;
        }
    }
}
