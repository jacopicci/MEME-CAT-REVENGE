using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattoPC : MonoBehaviour
{

    [SerializeField] GameObject gattoEnergy;
    [SerializeField] int tempoEnergia = 10;
    // Start is called before the first frame update

    void Update()
    {
        StartCoroutine(a_gattoPC());
    }
    IEnumerator a_gattoPC()
    {

        yield return new WaitForSeconds(tempoEnergia);
        gattoEnergy.SetActive(true);
        gameObject.SetActive(false);
    }
}
