using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAi : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public Transform mCharacter;
    public DistanceAi isNear;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNear.isNear)
        {
            meshAgent.SetDestination(mCharacter.position+ new Vector3(0, 2, 0));
        }
    }
}
