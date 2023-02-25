using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newRow : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go=Instantiate(prefab, transform.position, Quaternion.Euler(0,90,0));
        go.GetComponent<Rigidbody>().AddForce(Vector3.back, ForceMode.Impulse);
    }

}
