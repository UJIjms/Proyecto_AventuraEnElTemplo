using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextText : MonoBehaviour
{
    public GameObject text;
    public GameObject back;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            text.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
