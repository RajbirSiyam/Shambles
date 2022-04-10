using UnityEngine;

public class Throwables : MonoBehaviour
{
    [SerializeField] GameObject ninjaStarPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] Keybinds keys;

    public float throwForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(keys.Shoot))
        {
            Throw ();
        }
    }

    void Throw()
    {
        GameObject ninjaStar = Instantiate(ninjaStarPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = ninjaStar.GetComponent<Rigidbody>();

        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
    }
}
