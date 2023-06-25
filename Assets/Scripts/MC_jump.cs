using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_jump : MonoBehaviour
{
    public MC_Controler MC_Movement;

    private void OnTriggerStay(Collider other)
    {
        MC_Movement.iCanJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        MC_Movement.iCanJump = false;
    }
}
