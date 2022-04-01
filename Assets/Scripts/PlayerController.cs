using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform orientation;
    public Rigidbody rb;

    Vector3 moveDirection;

    [SerializeField] float z, x;
    public float moveSpeed;


    void Update()
    {
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");

        moveDirection = orientation.forward * x + orientation.right * z;
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }
}
