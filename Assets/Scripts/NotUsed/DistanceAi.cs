using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAi : MonoBehaviour
{
    public bool isNear = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }
}
