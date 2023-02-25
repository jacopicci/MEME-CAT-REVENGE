using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattoPCEnergy : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    [SerializeField] GameObject objectToDeactivate;
    [SerializeField] GameObject PM;

    private void Start()
    {
        PM = GameObject.Find("PlayerManager");
    }
    private void OnMouseDown()
    {
        

        PM.GetComponent<PlayerManager>().OneCoin();
        

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }


}
