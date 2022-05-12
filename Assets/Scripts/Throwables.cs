using UnityEngine;

public class Throwables : MonoBehaviour
{
    [SerializeField] GameObject ninjaStarPrefab;
    [SerializeField] Keybinds keys;
    public Transform throwPoint;

    public float throwForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keys.Shoot))
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject ninjaStar = Instantiate(ninjaStarPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = ninjaStar.GetComponent<Rigidbody>();

        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
    }
}
