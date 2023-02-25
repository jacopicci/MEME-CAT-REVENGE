using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMortaio : MonoBehaviour
{
    Vector3 scale;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 1.2f);
    }
    private void Update()
    {
        scale = transform.localScale;
        scale.x = scale.y = scale.z += Time.deltaTime;
        transform.localScale = scale;

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {
            other.GetComponent<units_base>().TakeDamage(60);
            
        }
    }
}
