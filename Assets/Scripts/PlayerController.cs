using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] RaycastHit wallLeftHit, wallRightHit;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform orientation, feet;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Animator animator;
    [SerializeField] Keybinds keys;
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;

    public float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float slideTimer;
    public float slideTimerIncrease;
    public float slideTimerDecrease;
    public float crouchSpeed;
    public float jumpForce;
    public float acceleration;
    public float gravityForce;
    public float tilt;
    public float camTilt;
    public float camTiltTime;
    [SerializeField] float z, x;

    [SerializeField] bool isGrounded;
    [SerializeField] bool sprinting;
    [SerializeField] bool sliding;
    [SerializeField] bool crouched;
    [SerializeField] bool wallRunning;
    [SerializeField] bool jumped;
    [SerializeField] bool wallLeft;
    [SerializeField] bool wallRight;


    void Update()
    {
        PlayerInput();
        PlayerAnimator();

        WallRunCheck();
    }

    void PlayerInput()
    {
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(feet.position, 0.5f, whatIsGround);

        moveDirection = orientation.forward * x + orientation.right * z;

        if (Input.GetKeyDown(keys.Jump) && isGrounded && !crouched && !sliding)
        {
            jumped = true;
        }
        else if (Input.GetKeyUp(keys.Jump))
        {
            jumped = false;
        }
    }

    void PlayerAnimator()
    {
        animator.SetFloat("Speed", x);
    }

    void FixedUpdate()
    {
        Movement();
    }

    //Wall Run
    void WallRunCheck()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, 0.7f);
        wallRight = Physics.Raycast(transform.position, orientation.right, 0.7f);

        if (wallLeft)
        {
            WallRun();
        }

        else if (wallRight)
        {
            WallRun();
        }

        else
        {
            wallRunning = false;
            gravityForce = 0.7f;

            tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        }
    }

    void WallRun()
    {
        wallRunning = true;
        gravityForce = 0.1f;

        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }

        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }
    }

    void Movement()
    {
        //Movement
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        rb.velocity = new Vector3(moveDirection.x * speed * Time.deltaTime, rb.velocity.y - gravityForce, moveDirection.z * speed * Time.deltaTime);

        //Jump
        if (jumped)
        {
            rb.velocity = Vector3.up * jumpForce * Time.deltaTime;
            jumped = false;
        }

        //Sprint
        if (Input.GetKey(keys.Sprint))
        {
            speed = sprintSpeed;
            sprinting = true;

            if (!sliding && slideTimer < 10f)
            {
                 slideTimer = slideTimer + slideTimerIncrease * Time.deltaTime;
            }
        }
        else
        {
            speed = walkSpeed;
            sprinting = false;
            slideTimer = 0;
        }

        //Crouch & Slide
        if (Input.GetKey(keys.Crouch))
        {
            playerCollider.center = new Vector3(0, 1.48f, 0);
            playerCollider.height = 2;

            //Sliding
            if(sprinting && slideTimer > 0f)
            {
                slideTimer = slideTimer - slideTimerDecrease * Time.deltaTime;
                speed = slideSpeed;
                sliding = true;
            }

            //Crouching
            else
            {
                speed = crouchSpeed;
                crouched = true;
            }
        }
        else
        {
            playerCollider.height = 3;
            crouched = false;
            sliding = false;
        }
    }
}