using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caneBonk : MonoBehaviour
{
    Rigidbody rb;
    bool move = true;
    bool attack;
    [SerializeField] float rateo;
    [SerializeField] int danno;
    [SerializeField] float speed = 1;
    Collider Collidercol;
    float speedMultiplier = 1;

    bool onCD;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    float timer;
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
        if (!attack && !onCD)
        {
            StartCoroutine(dmg());
        }
        if (attack && Collidercol != null)
        {
            Debug.Log("work");
            move = Collidercol.gameObject.GetComponentInParent<units_base>().TakeDamage(danno);
            GetComponent<AudioSource>().Play();

            attack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ally")
        {
            Collidercol = other;
            move = false;

            attack = true;
            GetComponentInChildren<Animator>().SetTrigger("Bonk");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Untagged" && other.tag != "Ally") move = true;
    }
    IEnumerator dmg()
    {
        onCD = true;
        GetComponentInChildren<Animator>().ResetTrigger("Bonk");

        yield return new WaitForSeconds(rateo);
        attack = true;
        onCD = false;
    }
}
