using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    public int points = 0;
    private int enemyCount = 0;
    private int lastCount = 0;
    private TextMeshProUGUI uiText;

    private GameObject MCharacter;
    private MC_Controler myScript;

    private void Start()
    {
        MCharacter = GameObject.Find("ManiCharacter");
        myScript = MCharacter.GetComponent<MC_Controler>();
        uiText = this.GetComponent<TextMeshProUGUI>();
        uiText.text = "Puntos: " + points.ToString();
    }

    void Update()
    {

        if (points < myScript.points)
        {
            points = myScript.points;
            uiText.text = "Puntos: " + points.ToString();
        }      
    }
}
