using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MC_Controler : MonoBehaviour
{
    private Rigidbody rB;
    private Animator anim;
    private float movementSpeed = 4f;
    private float mHor;
    private float mVer;
    private bool running;

    public GameObject sliderLife;
    public float life = 20F;
    public GameObject sliderStamina;
    public float stamina = 20F;
    public Transform characterCamera;
    public float jumpStrengh = 8f;
    public bool iCanJump;
    public Vector3 direction;
    public GameObject hand;

    public GameObject Arrow;
    public Transform spawnArrowPoint;
    public float arrowVelocity = 50F;

    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        iCanJump = false;
        rB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hand.gameObject.SetActive(false);
        Invoke("staminaRecuperation", 0.1F);
        running = false;
    }

    void FixedUpdate()
    {
        Quaternion newRotation = characterCamera.rotation;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = newRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            this.transform.position = new Vector3(230f, -1f, 700f);
            life = 20;
            stamina = 20;
            points -= 30;
        }

        UpdateCanva();

        direction = transform.forward;
        mHor = Input.GetAxis("Horizontal");
        mVer = Input.GetAxis("Vertical");
        float run = Input.GetAxis("Fire1");

        if (mHor != 0 || mVer != 0)
        {
            if (run == 1 && stamina > 0)
            {
                running = true;
                movementSpeed = 8;
                anim.SetBool("isRunning", true);
            }
            else
            {
                running = false;
                movementSpeed = 4;
                anim.SetBool("isRunning", false);
            }
        }
        
        if (run == 0)
        {
            anim.SetFloat("XSpeed", mHor);
            anim.SetFloat("YSpeed", mVer);
        }
        else
        {
            anim.SetFloat("XSpeed", mHor * 2);
            anim.SetFloat("YSpeed", mVer * 2);
        }

        transform.Translate(mHor * Time.deltaTime * movementSpeed, 0, mVer * Time.deltaTime * movementSpeed);

        if (iCanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isJumping", true);
                rB.AddForce(new Vector3(0, jumpStrengh, 0), ForceMode.Impulse);
            }
            anim.SetBool("inFloor", true);
        }
        else
        {
            ImFalling();
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Kik");
            hand.gameObject.SetActive(true);
            Invoke("DeactivateObject", 0.5F);

        }

        if (Input.GetMouseButtonDown(1) && stamina >= 5)
        {
            anim.SetTrigger("Throw");
            stamina -= 5;
        }
    }

    public void ImFalling()
    {
        anim.SetBool("inFloor", false);
        anim.SetBool("isJumping", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            life -= 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            life -= 3;
        }
    }

    private void DeactivateObject()
    {
        hand.SetActive(false);
    }

    private void UpdateCanva()
    {
        sliderLife.GetComponent<Slider>().value = life;
        sliderStamina.GetComponent<Slider>().value = stamina;
    }

    void staminaRecuperation()
    {
        if (stamina < 20 && !running) stamina += 0.1F;
        if (running) stamina -= 0.3F;

        Invoke("staminaRecuperation", 0.1F);
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(Arrow, spawnArrowPoint.position, spawnArrowPoint.rotation);
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        Quaternion initialRotation = newBullet.transform.rotation;
        newBullet.transform.rotation = initialRotation * Quaternion.Euler(90f, 0f, 0f); 
        Vector3 shootDirection = characterCamera.forward;
        bulletRigidbody.AddForce(shootDirection * arrowVelocity, ForceMode.Impulse);
    }
}
