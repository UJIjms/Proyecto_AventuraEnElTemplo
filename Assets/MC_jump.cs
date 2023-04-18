using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_jump : MonoBehaviour
{
    public MC_movement MC_Movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        MC_Movement.iCanJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        MC_Movement.iCanJump = false;
    }
}
