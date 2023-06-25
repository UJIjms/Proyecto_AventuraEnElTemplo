using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform spawnBulletPoint;
    public float bulletVelocity = 100F;

    private Transform target;

    void Start()
    {
     target = GameObject.Find("ManiCharacter").transform;
        Invoke("Shoot", 3);
    }

    void Update()
    {
 
    }

    void Shoot()
    {
        Vector3 targetDirection = target.position - transform.position;
        GameObject newBullet;
        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(targetDirection * bulletVelocity, ForceMode.Force);
        Invoke("Shoot", 3);
    }
}
