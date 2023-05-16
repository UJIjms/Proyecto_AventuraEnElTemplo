using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAi : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public Transform mCharacter;
    public DistanceAi isNear;
    public MC_movement CharacterCode;

    private int life = 3;


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionThis = transform.position - mCharacter.transform.position;
        float angulo = Vector3.Angle(CharacterCode.direction, directionThis);

        if (isNear.isNear)
        {
            if (CharacterCode.atack && angulo <= 10)
            {
                life -= 1;
                if(life == 0)
                {
                    this.gameObject.SetActive(false);
                }                
            }
            else
            {
                meshAgent.SetDestination(mCharacter.position + new Vector3(0, 2, 0));
            }
        }            
    }
}
