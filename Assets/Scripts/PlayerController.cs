using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform orientation, feet;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Keybinds keys;
    [SerializeField] Rigidbody rb;

    Vector3 moveDirection;

    [SerializeField] float z, x;
    private bool jumped;
    [SerializeField] bool isGrounded, sprinting, crouched;
    public float speed, walkSpeed, sprintSpeed, crouchSpeed, jumpForce, acceleration, gravityForce;
    

    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(feet.position, 0.5f, whatIsGround);

        moveDirection = orientation.forward * x + orientation.right * z;

        if(Input.GetKeyDown(keys.Jump) && isGrounded && !crouched)
        {
            jumped = true;
        }
        else if (Input.GetKeyUp(keys.Jump))
        {
            jumped = false;
        }
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
        if(jumped && !crouched)
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
        if(Input.GetKey(keys.Crouch))
        {
            playerCollider.height = 0.065f;
            speed = crouchSpeed;
            crouched = true;
        }
        else
        {
            playerCollider.height = 0.13f;
            crouched = false;
        }

        //Sliding
    }
}
