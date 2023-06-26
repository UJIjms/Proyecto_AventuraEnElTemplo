using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceEnemyAI : MonoBehaviour
{
    private int routines;
    private float corono;
    private Quaternion angle;
    public float grade;
    private Animator ani;

    public GameObject enemyBullet;
    public Transform spawnBulletPoint;
    public float bulletVelocity = 100F;
    public GameObject target;

    public GameObject slider;
    public int life = 10;

    private Transform targetPos;    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("ManiCharacter");
        targetPos = GameObject.Find("ManiCharacter").transform;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) this.gameObject.SetActive(false);
        Enemy_Behavior();
        RotateCanva();
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
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);

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
    void Shoot()
    {
        Vector3 targetDirection = targetPos.position - transform.position;
        Vector3 adjust = new Vector3(-1f, 0f, 0f);
        targetDirection += adjust;
        GameObject newBullet;
        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);

        Quaternion initialRotation = newBullet.transform.rotation;
        newBullet.transform.rotation = initialRotation * Quaternion.Euler(90f, 0f, 0f);

        newBullet.GetComponent<Rigidbody>().AddForce(targetDirection * bulletVelocity, ForceMode.Force);
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
