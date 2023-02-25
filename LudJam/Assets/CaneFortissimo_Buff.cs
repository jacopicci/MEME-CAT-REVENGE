using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneFortissimo_Buff : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<units_base>().speedMultiplier = 2;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            units_base baseScript = other.gameObject.GetComponent<units_base>();
        if (baseScript != null)
            {
                baseScript.speedMultiplier = 2;
                
            }
        
        }
    }
}
