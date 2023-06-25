using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Controler : MonoBehaviour
{
    private Rigidbody rB;
    private Animator anim;
    private float movementSpeed = 4f;
    private float mHor;
    private float mVer;

    public float life = 20F;
    public bool atack; 
    public Transform characterCamera;
    public float jumpStrengh = 8f;
    public bool iCanJump;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        iCanJump = false;
        rB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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
            this.gameObject.SetActive(false);
        }

        direction = transform.forward;
        mHor = Input.GetAxis("Horizontal");
        mVer = Input.GetAxis("Vertical");
        float run = Input.GetAxis("Fire1");

        if (mHor != 0 || mVer != 0)
        {
            if (run == 1)
            {
                movementSpeed = 8;
                anim.SetBool("isRunning", true);
            }
            else
            {
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

        if (Input.GetMouseButtonDown(0)) // Verifica si se presiona el botón derecho del mouse
        {
            atack = true;
        }
        else
        {
            atack = false;
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
}
