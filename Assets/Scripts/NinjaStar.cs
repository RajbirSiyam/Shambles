using UnityEngine;

public class NinjaStar : MonoBehaviour
{
    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(25f, -25f));
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 1500 * Time.deltaTime);

        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.05f);
    }
}
