using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyAI : MonoBehaviour
{
    private int routines;
    private float corono;
    private Quaternion angle;
    public float grade;
    private Animator ani;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("ManiCharacter");
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_Behavior();
    }

    private void Enemy_Behavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 25)
        {
            ani.SetBool("throw", false);

            ani.SetBool("run", false);
            corono += 1 * Time.deltaTime;
            if (corono >= 4)
            {
                routines = Random.Range(0, 2);
                corono = 0;
            }
            switch (routines)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    routines++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5F);
                    transform.Translate(Vector3.forward * 3 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            if (Vector3.Distance(transform.position, target.transform.position) > 15)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 9 * Time.deltaTime);

                ani.SetBool("throw", false);
            }
            else if (Vector3.Distance(transform.position, target.transform.position) < 10)
            {
                Quaternion reverseRotation = Quaternion.Euler(0, 180, 0) * rotation;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, reverseRotation, 20);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);

                ani.SetBool("throw", false);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20);                
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("throw", true);
            }
        }
    }
}
