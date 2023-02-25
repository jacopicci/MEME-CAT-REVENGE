using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colpoMortaio : MonoBehaviour
{
    System.Random rand = new System.Random();
    Rigidbody rb;
    [SerializeField] GameObject boom;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 rot= transform.rotation * -Vector3.left;
        rot.z += rand.Next(10, 20)/10;
        rb.AddForce(rot*400, ForceMode.Impulse);

    }
    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Terrain") 
        {
            Instantiate(boom, transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(gameObject);
        }

    }
}
