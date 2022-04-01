using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform orientation, feet;
    public LayerMask whatIsGround;
    public Rigidbody rb;

    Vector3 moveDirection;

    [SerializeField] float z, x;
    [SerializeField] bool jumped, isGrounded;
    public float moveSpeed, jumpForce, gravityForce;


    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void PlayerInput()
    {
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(feet.position, 0.25f, whatIsGround);

        moveDirection = orientation.forward * x + orientation.right * z;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumped = true;
        }
    }

    void Movement()
    {

        rb.velocity = new Vector3(moveDirection.x * moveSpeed * Time.deltaTime, rb.velocity.y - gravityForce, moveDirection.z * moveSpeed * Time.deltaTime);

        if(jumped)
        {
            rb.velocity = Vector3.up * jumpForce * Time.deltaTime;
            jumped = false;
        }
    }
}
