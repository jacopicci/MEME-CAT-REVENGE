using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoFortissimo : MonoBehaviour
{
    Rigidbody rb;
    bool move = true;
    bool attack;
    [SerializeField] float rateo;
    [SerializeField] int danno;
    [SerializeField] float speed = 1;
    Collision Collidercol;
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
            //Debug.Log("work");
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("Attack");
            move = Collidercol.gameObject.GetComponentInParent<units_base>().TakeDamage(danno);

            attack = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ally")
        {
            Collidercol = other;
            move = false;

            attack = true;

        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "Untagged" && other.gameObject.tag != "Ally") move = true;
    }

    IEnumerator dmg()
    {
        onCD = true;
        yield return new WaitForSeconds(rateo);
        attack = true;
        onCD = false;
    }
}
