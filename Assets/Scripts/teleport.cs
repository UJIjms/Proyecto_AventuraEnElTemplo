using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform Target;
    public GameObject Player;
    Vector3 dsitance = new Vector3(5,0,5);
    private void OnTriggerEnter(Collider other)
    {
        Player.transform.position = Target.transform.position + dsitance;
    }
}
