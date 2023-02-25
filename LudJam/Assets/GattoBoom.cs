using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattoBoom : MonoBehaviour
{
    List<Collider> colliders;
    bool used;

    private Animator animator;
    units_base baseScript;

    void Start()
    {
        colliders = new List<Collider>();
        used = false;

        animator = GetComponent<Animator>();
        baseScript = GetComponentInParent<units_base>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            colliders.Add(other);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!used && collision.gameObject.tag == "Enemy")
        {
            
            used = true;
            GetComponent<Animator>().SetTrigger("Boom");
            StartCoroutine(boom());
        }
    }
    IEnumerator boom()
    {
        yield return new WaitForSeconds(5f);
        AudioSource audioSource = GetComponentInParent<AudioSource>();
        audioSource.Play();

        for (int i = 0; i < colliders.Count; i++)
        {
            units_base go;
            if (colliders[i] != null)
            {
                if (colliders[i].gameObject.GetComponent<units_base>() != null)
                {
                    go = colliders[i].gameObject.GetComponent<units_base>();
                    go.TakeDamage(200);
                }
                else
                {
                    go = colliders[i].gameObject.GetComponentInChildren<units_base>();
                    if (go != null)
                    {
                        go.TakeDamage(200);
                    }
                }
            }
        }

        Debug.Log("testing");
        baseScript.caselFree();
        baseScript.TakeDamage(1000);

        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }

}
