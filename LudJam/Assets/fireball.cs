using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] int damageAmount = 10;
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] bool isColtello;
    public int dmgMult = 1;

    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!isColtello)
        {
            rb.velocity = -Vector3.left * projectileSpeed;
        }
        if (isColtello)
        {
            Quaternion knifeRotation = transform.rotation;
            rb.velocity = knifeRotation * -Vector3.left * projectileSpeed;
        }
        Destroy(gameObject, 3f);
    }



    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            units_base enemyHealth = collision.gameObject.GetComponentInParent<units_base>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount * dmgMult);
            }
            
        }
    }
}
