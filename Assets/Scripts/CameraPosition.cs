using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Transform camPos;

    void Update()
    {
        transform.position = camPos.position;
        transform.rotation = camPos.rotation;
    }
}
