using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public BasicEnemyAi EnemyCode;
    public Transform objectTransform;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = objectTransform.localScale;
        newScale.y = EnemyCode.life / 10F;
        objectTransform.localScale = newScale;
    }
}
