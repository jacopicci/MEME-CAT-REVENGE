using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class caneBoom : MonoBehaviour
{
    Rigidbody rb;
    bool move = true;
    bool attack;
    [SerializeField] int danno;
    [SerializeField] float speed = 1;
    Collider Collidercol;
    float speedMultiplier = 1;
    bool used;
    [SerializeField] AudioClip Audioclip;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        speedMultiplier = GetComponent<units_base>().speedMultiplier;
        if (move)
        {
            rb.velocity = Vector3.left * speed * speedMultiplier;
        }
        else
        {
            rb.velocity = Vector3.zero;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (!used && other.gameObject.tag == "Ally")
        {
            Debug.Log("Boomused:" + used);
            move = false;
            used = true;
            GetComponentInChildren<Animator>().SetTrigger("Boom");
            
            StartCoroutine(boom(other));
        }
    }
    IEnumerator boom(Collider colliders)
    {
        yield return new WaitForSeconds(3.5f);

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        units_base go = colliders.gameObject.GetComponent<units_base>();
        if (go != null)
        {
            go.TakeDamage(danno);
        }
        else
        {
            go = colliders.gameObject.GetComponentInChildren<units_base>();
            if (go != null)
            {
                go.TakeDamage(danno);
            }
        }

        Debug.Log("testing");
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }


}

