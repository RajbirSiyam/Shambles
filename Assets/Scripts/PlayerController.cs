using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform orientation, feet;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Animator animator;
    [SerializeField] Keybinds keys;
    [SerializeField] Rigidbody rb;


    public float speed, walkSpeed, sprintSpeed, crouchSpeed, jumpForce, acceleration, gravityForce;
    [SerializeField] bool isGrounded, sprinting, crouched;
    [SerializeField] float z, x;
    private bool jumped;


    void Update()
    {
        PlayerInput();
        PlayerAnimator();
    }

    void PlayerInput()
    {
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(feet.position, 0.5f, whatIsGround);

        moveDirection = orientation.forward * x + orientation.right * z;

        if (Input.GetKeyDown(keys.Jump) && isGrounded && !crouched)
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


    void Movement()
    {
        //Movement
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        rb.velocity = new Vector3(moveDirection.x * speed * Time.deltaTime, rb.velocity.y - gravityForce, moveDirection.z * speed * Time.deltaTime);

        //Jump
        if (jumped && !crouched)
        {
            rb.velocity = Vector3.up * jumpForce * Time.deltaTime;
            jumped = false;
        }

        //Sprint
        if (Input.GetKey(keys.Sprint) && !crouched)
        {
            speed = Mathf.Lerp(walkSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(sprintSpeed, walkSpeed, acceleration * Time.deltaTime);
        }

        //Crouch
        if (Input.GetKey(keys.Crouch))
        {
            playerCollider.height = 2;
            playerCollider.center = new Vector3(0, 1.95f, 0);
            speed = crouchSpeed;
            crouched = true;
        }
        else
        {
            playerCollider.height = 3;
            crouched = false;
        }

        //Sliding
    }
}
