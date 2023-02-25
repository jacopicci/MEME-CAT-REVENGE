using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattoPistola : MonoBehaviour
{
    int x;
    int y;
    units_base scriptBase;
    System.Random rnd = new System.Random();
    [SerializeField] float rateo;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject startingPos;
    bool shooting;
    float RateoMult = 1;
    int dmgMult = 1;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            shooting = true;
            StartCoroutine(spara());
            
        }
    }
    private void FixedUpdate()
    {
        RateoMult = GetComponentInParent<units_base>().RateoMult;
        dmgMult = (int)Mathf.Round(GetComponentInParent<units_base>().dmgMultiplier);
        PallaDiFuoco = GetComponentInParent<units_base>().percentualePallaDiFuoco;
    }
    IEnumerator spara()
    {
        FireBallNoJutsu();

        GetComponentInParent<AudioSource>().Play();
        GameObject go = Instantiate(prefab, startingPos.transform.position, Quaternion.identity);
        go.GetComponent<Proiettile>().dmgMult= dmgMult;
        GetComponentInChildren<Animator>().SetTrigger("Spara");
        yield return new WaitForSeconds(0.17f);
        GetComponentInChildren<ParticleSystem>().Play();
        
        yield return new WaitForSeconds(rateo*(1/ RateoMult));
        shooting = false;
    }
    [SerializeField] GameObject Fireball;
    float PallaDiFuoco = 0;

    void FireBallNoJutsu()
    {
        if (Random.value < PallaDiFuoco)
        {
            Instantiate(Fireball, startingPos.transform.position, Quaternion.identity);
        }
    }
}
