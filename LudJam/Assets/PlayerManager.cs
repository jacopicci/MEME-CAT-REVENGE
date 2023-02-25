using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI moneyText;
    int money = 1;
    [SerializeField] GameObject[] arrayAllies;
    unitsInShop[] actualUnits = new unitsInShop[8];
    [SerializeField] Sprite[] actualSprites;
    [SerializeField] Image prefabSprite;
    [SerializeField] GameObject HUD;
    [SerializeField] caselleScacchiera cs;
    [SerializeField] LayerMask lm;
    [SerializeField] Upgrades upgs;
    Image tempGO = null;
    int clicked;
    public bool boughtToSpawn;
    public bool boughtToDestroy;
    public int PassiveGain;
    bool coroutineOn;
    [SerializeField] AudioMixerSnapshot amsnap;
    
    private void Start()
    {
        amsnap.TransitionTo(2);
        actualUnits[0] = new unitsInShop(1, 1, arrayAllies[1]); //GattoPC
        actualUnits[1] = new unitsInShop(2, 2, arrayAllies[2]); //GattoBase
        actualUnits[2] = new unitsInShop(4, 3, arrayAllies[3]); //GattoPistola
        actualUnits[3] = new unitsInShop(5, 4, arrayAllies[4]); //GattoBoom
        actualUnits[4] = new unitsInShop(4, 5, arrayAllies[5]); //GattoCursed
        actualUnits[5] = new unitsInShop(3, 6, arrayAllies[6]); //GattoAlto
        actualUnits[6] = new unitsInShop(8, 7, arrayAllies[7]); //gattoMortaio
        actualUnits[7] = new unitsInShop(0, 0, null);

    }

    // Update is called once per frame
    void Update()
    {
        money = Mathf.Clamp(money, 0, 99);
        moneyText.text = money.ToString();
        if (tempGO == null && (boughtToSpawn||boughtToDestroy))
        {
            
            tempGO = Instantiate(prefabSprite, HUD.transform);
            
            
        }
        if (tempGO != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition.x += 40;
            mousePosition.y -= 40;
            tempGO.transform.position = mousePosition;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (boughtToSpawn)
                {
                    money += actualUnits[clicked].cost;
                }
                else if (!boughtToDestroy)
                {
                    Destroy(tempGO);
                }
                if (tempGO != null) Destroy(tempGO);
                boughtToSpawn = false;
                boughtToDestroy = false;
            }
        }

        if (PassiveGain != 0 && !coroutineOn)
        {
            coroutineOn= true;
            StartCoroutine(passiveCoin());
        }

        if (boughtToDestroy)
        {
            if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, lm))
                {
                    Debug.Log(hit.collider.gameObject);
                    
                    hit.collider.gameObject.GetComponentInParent<units_base>().caselFree();
                    Destroy(hit.collider.gameObject.transform.parent.gameObject);
                    Destroy(tempGO);
                    boughtToDestroy= false;
                }
            }
        }
        else if (!boughtToSpawn)
        {
            Destroy(tempGO);
        }


    }
    public void DestroySpray()
    {
        if (!boughtToSpawn && !boughtToDestroy)
        {
            boughtToDestroy = true;
            prefabSprite.GetComponentInChildren<Image>().sprite = actualSprites[8];
        }
        
    }
    public void DeleteNearestObject(Vector3 trasf)
    {
        // Impostare il raggio di sfera con un raggio di 1 unità
        float radius = Mathf.Infinity;
        
        // Lanciare lo SphereCast dalla posizione dell'osservatore
        RaycastHit hitInfo;
        if (Physics.SphereCast(trasf, radius,Vector3.zero, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            // Ottenere il GameObject colpito dallo SphereCast
            GameObject nearestObject = hitInfo.collider.gameObject;

            // Eliminare l'oggetto trovato
            if (nearestObject != null)
            {
                GameObject.Destroy(nearestObject);
            }
        }
    }

    public void OneCoin()
    {
        //Debug.Log("Daje");
        money += 1;
    }
    IEnumerator passiveCoin()
    {
        money += PassiveGain;
        yield return new WaitForSeconds(15);
        coroutineOn= false;
    }
    public void onClick(int a)
    {
        //Debug.Log("1");
        if (!boughtToSpawn && !boughtToDestroy)
        {
            clicked = a;
            prefabSprite.GetComponentInChildren<Image>().sprite = actualSprites[a];
            if (money >= actualUnits[clicked].cost && actualUnits[clicked].go != null)
            {
                money -= actualUnits[clicked].cost;
                boughtToSpawn = true;
            }
        }

    }

    public void spawning(Vector3 spawnPos, int x, int y)
    {

        //Debug.Log(actualUnits[clicked].go);
        Destroy(tempGO);
        tempGO = null;
        GameObject go = Instantiate(actualUnits[clicked].go, spawnPos, actualUnits[clicked].go.transform.rotation);
        units_base basescript = go.GetComponent<units_base>();
        basescript.boardScript = cs;
        basescript.upgs = upgs;
        basescript.posizione.posX = x;
        basescript.posizione.posY =y;
        basescript.isEnemy= false;
        boughtToSpawn = false;
    }

    unitsInShop shopInitializer(int i) //quando ho fatto il menu e la selezione delle unità
    {

        return null;
        
    }
}

public class unitsInShop
{
    public int cost;
    public int ABSunitArray;
    public GameObject go;
    public unitsInShop(int money, int ABSUnitArray, GameObject go)
    {
        this.cost = money;
        this.ABSunitArray = ABSUnitArray;
        this.go = go;
    }
}

