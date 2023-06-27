using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleEnemyAI : MonoBehaviour
{
    private int routines;
    private float corono;
    private Quaternion angle;
    public float grade;
    private Animator ani;
    private bool attack;

    public GameObject target;

    public GameObject slider;
    public int life = 20;

    private int numericValue;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("ManiCharacter");
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            MC_Controler myScript = target.GetComponent<MC_Controler>();
            myScript.points += 20;
            this.gameObject.SetActive(false);
        }
        Enemy_Behavior();
        RotateCanva();
    }

    private void Enemy_Behavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 20 && life == 20) {
            End_Ani();

            ani.SetBool("run", false);
            corono += 1 * Time.deltaTime;
            if (corono >= 4)
            {
                routines = Random.Range(0,2);
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
            if (Vector3.Distance(transform.position, target.transform.position) > 1.5 && !attack)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 9 * Time.deltaTime);

                ani.SetBool("attack", false);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20);
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                attack = true;
            }
        }
    }
    public void End_Ani()
    {
        ani.SetBool("attack", false);
        attack = false;
    }

    private void RotateCanva()
    {
        slider.GetComponent<Slider>().value = life;
        var lookPos = target.transform.position - transform.position;
        var rotationCanva = Quaternion.LookRotation(lookPos);
        slider.transform.rotation = rotationCanva;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MCweapon"))
        {
            life -= 2;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MCweapon"))
        {
            life -= 5;
        }
    }
}
