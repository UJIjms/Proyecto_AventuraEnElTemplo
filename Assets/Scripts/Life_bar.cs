using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_bar : MonoBehaviour
{
    public MC_Controler CharacterCode;
    public RectTransform rectangleTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newWidth = 30F * CharacterCode.life;
        rectangleTransform.sizeDelta = new Vector2(newWidth, rectangleTransform.sizeDelta.y);
    }
}
