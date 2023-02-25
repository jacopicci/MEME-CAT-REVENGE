using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{

    System.Random rand = new System.Random();
    [SerializeField] PlayerManager pm;
    [SerializeField]
    Sprite[] sprites;
    public delegate void upgmethods();
    public upgmethods[] UpgradeMethodsArray = new upgmethods[5];
    [SerializeField] GameObject UpgsHUD;
    [SerializeField] GameObject[] buttons;
    public float ATKspeedMult = 1;
    public float speedMult = 1;
    int counter;
    public float dmgMult = 1;
    public bool upgrading;
    string[] strings = new string[5];
    public float percentualePallaDifuoco;
    List<int> selectedIndices;
    
    private void Start()
    {
        UpgradeMethodsArray[0] = ErbaGatta;
        UpgradeMethodsArray[1] = Polpetteavvelenate;
        UpgradeMethodsArray[2] = MrIncredibleBecomingUncanny;
        UpgradeMethodsArray[3] = GattiInformatici;
        UpgradeMethodsArray[4] = PallaDiFuoco;



        strings[0] = "Aumenta fire/attack rate dei gatti";//Erba Gatta
        strings[1] = "I cani subiscono danno extra";//Polpetteavvelenate
        strings[2] = "Tutte le unità nemiche sono rallentate";//MrIncredibleBecomingUncanny
        strings[3] = "I gatti hanno hackerato il dogeCoin, guadagni monete bonus passivamente";//MrIncredibleBecomingUncanny
        strings[4] = "I tuoi gatti guardano troppo naruto? Katon goukakyuu no jutsu";//MrIncredibleBecomingUncanny

    }
    public IEnumerator upgGenerator() //parte da scacchiera ogni %x wave
    {
        pm.boughtToSpawn = false;
        pm.boughtToDestroy = false;
        Debug.Log(percentualePallaDifuoco);
        Debug.Log(ATKspeedMult);
        Debug.Log(speedMult);
        Debug.Log(dmgMult);
        Time.timeScale = 0;
        upgrading = true;
        if (counter <= UpgradeMethodsArray.Length-2)
        {
            selectedIndices = new List<int>(); // inizializza la lista vuota
            for (int i = 0; i < 3; i++)
            {

                int upgsSelected;
                do
                {
                upgsSelected = rand.Next(0, UpgradeMethodsArray.Length);
                yield return null;

                } while (UpgradeMethodsArray[upgsSelected] == null || selectedIndices.Contains(upgsSelected));

                selectedIndices.Add(upgsSelected);

                Transform childTransform = buttons[i].transform.Find("Image");
                childTransform.GetComponent<Image>().sprite = sprites[upgsSelected];
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = strings[upgsSelected];
            }
            
            yield return null;
            UpgsHUD.SetActive(true);
        }
        yield return null;

    }
    public void OnClick(int clicked)
    {
        
        UpgradeMethodsArray[selectedIndices[clicked]]();
        selectedIndices.Clear();
        upgrading = false;
        UpgsHUD.SetActive(false);
        Time.timeScale = 1;
    }

    int upg0Level;
    void ErbaGatta()
    {
        //aumenta il fire rate del 20%
        if (upg0Level == 0)
        {
            ATKspeedMult = 1.25f;
            upg0Level += 1;
        }
        else if (upg0Level == 1)
        {
            ATKspeedMult = 1.50f;
            upg0Level += 1;

        }
        else if (upg0Level == 2)
        {
            speedMult = 2;
            UpgradeMethodsArray[0] = null;
            counter += 1;
        }
    }
    int upg1Level;
    void Polpetteavvelenate()
    {
        //Danno Extra
        if (upg1Level == 0)
        {
            upg1Level += 1;
        }
        else if (upg1Level == 1)
        {
            upg1Level += 1;
        }
        else if (upg1Level == 2)
        {
            UpgradeMethodsArray[1] = null;
            counter += 1;
        }
    }
    int upg2Level;
    void MrIncredibleBecomingUncanny()
    {
        //rallentamento Nemici
        if (upg2Level == 0)
        {
            speedMult = 0.75f;
            upg2Level += 1;
        }
        else if (upg2Level == 1)
        {
            speedMult = 0.60f;
            upg2Level += 1;
        }
        else if (upg2Level == 2)
        {
            UpgradeMethodsArray[2] = null;
            speedMult = 0.45f;
            counter += 1;
        }

    }
    int upg3Level;

    void PallaDiFuoco()
    {
        //ProiettiliTrapassano i nemici

        if (upg3Level == 0)
        {
            percentualePallaDifuoco = 0.1f;
            upg3Level += 1;
        }
        else if (upg3Level == 1)
        {
            percentualePallaDifuoco = 0.2f;
            upg3Level += 1;
        }
        else if (upg3Level == 2)
        {
            UpgradeMethodsArray[3] = null;
            percentualePallaDifuoco = 0.3f;
            counter += 1;
        }
    }
    int upg4Level;
    
    void GattiInformatici()
    {
        //coinPerSeconds

        if (upg4Level == 0)
        {
            pm.PassiveGain = 1;
            upg4Level += 1;
        }
        else if (upg4Level == 1)
        {
            pm.PassiveGain = 2;
            upg4Level += 1;
        }
        else if (upg4Level == 2)
        {
            pm.PassiveGain = 3;
            UpgradeMethodsArray[4] = null;
            counter += 1;
        }
    }
}

