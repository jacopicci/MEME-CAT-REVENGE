using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class units_base : MonoBehaviour
{
    bool isDead;
    [SerializeField] int hp =100;
    public casella posizione = new casella("A0", false, "", null, 0, 0, 100);
    public caselleScacchiera boardScript;
    public Upgrades upgs;
    public bool isPrefab;
    public bool isEnemy;

    [SerializeField] AudioClip AudioClipCani;
    [SerializeField] AudioClip AudioClipGatti;

    //public vars for upgrades
    public float RateoMult = 1;
    public float dmgMultiplier = 1;
    public float speedMultiplier = 1;
    public float percentualePallaDiFuoco = 0;
    
    private void Awake()
    {
        posizione.hp= hp;
    }
    private void FixedUpdate()
    {
        if (upgs != null)
        {
            RateoMult = upgs.ATKspeedMult;
            dmgMultiplier = upgs.dmgMult;
            speedMultiplier = upgs.speedMult;
            percentualePallaDiFuoco = upgs.percentualePallaDifuoco;
        }
    }
    public void caselFree()
    {
        boardScript.freeCasella(posizione.posX, posizione.posY);
    }

    public bool TakeDamage(int dmg)
    {
        posizione.hp -= dmg;
        if (posizione.hp <= 0)
        {
            isDead= true;
            AudioSource sfx = null;
            if (isEnemy)
            {
                sfx = GetComponent<AudioSource>();
                if (sfx == null) sfx = GetComponentInChildren<AudioSource>();
                if (sfx != null)
                {
                    sfx.clip = AudioClipCani;
                    sfx.Play();
                }
            }
            else
            {
                sfx = GetComponent<AudioSource>();
                if (sfx == null) sfx = GetComponentInChildren<AudioSource>();
                if (sfx != null)
                {
                    sfx.clip = AudioClipGatti;
                    sfx.Play();
                }
            }

                Invoke("DestroyGameObject", sfx.clip.length);

        }
        return isDead;
    }

    private void DestroyGameObject()
    {
        if (!isEnemy && boardScript != null)
        {
            boardScript.freeCasella(posizione.posX, posizione.posY);
        }
        Destroy(gameObject);
    }





}

