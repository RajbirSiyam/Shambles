using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] PlayerController playerController;
    [SerializeField] float sens, mouseX, mouseY, xRotation, yRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sens;
        xRotation -= mouseY * sens;  

        xRotation = Mathf.Clamp(xRotation, -90f, 60f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, playerController.tilt);
        orientation.rotation = Quaternion.Euler(0, yRotation, transform.rotation.z);
    }
}
