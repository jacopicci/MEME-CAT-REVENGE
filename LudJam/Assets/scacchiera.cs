using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

public class scacchiera : MonoBehaviour
{
    [SerializeField] GameObject[] enemyUnits;
    [SerializeField] Transform[] firstRow;
    caselleScacchiera casellScacch;
    Upgrades upgs;
    [SerializeField] GameObject spawner;

    int enemyNum;
    public List<int> EnemiesPos;
    private List<int> usedNumbers = new List<int>();
    private List<Transform> TransformPos = new List<Transform>();
    private List<int> enemiesTypeList = new List<int>();
    private System.Random random = new System.Random();
    private List<int> randomNumbers = new List<int>();
    float value;
    int usableValue;
    public int wave =1;
    bool coroutineRunning;
    int maxTierUnit;
    [SerializeField] TextMeshProUGUI waveCounter;
    [SerializeField] GameObject[] newRows;
    public int maxWave = 3;
    // Update is called once per frame
    private void Awake()
    {
        casellScacch = GetComponent<caselleScacchiera>();
        upgs = GetComponent<Upgrades>();
        

    }
    private void Update()
    {
        if (!coroutineRunning)
        {
            coroutineRunning = true;

            StartCoroutine(startingSpawn());

        }
        waveCounter.text = "Wave: " + wave;
    }

    IEnumerator startingSpawn()
    {
        yield return new WaitForSeconds(5);

        enemiesTypeList = enemySelection();
        EnemiesPos = GetRandomNumbers(enemiesTypeList.Count);
        TransformPos = intToTransform(EnemiesPos);


        //Debug.Log(enemiesTypeList.Count);
        float timer = 0f;
        for (int i = 0; i < enemiesTypeList.Count; i++)
        {
            GameObject myPrefab = enemyUnits[enemiesTypeList[i]];
            GameObject TempGo = Instantiate(myPrefab, TransformPos[i].position, Quaternion.identity);
            while (timer < random.Next(1, 2))
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            
            units_base baseScript = TempGo.GetComponent<units_base>();
            while (baseScript == null)
            {
                baseScript = TempGo.GetComponent<units_base>();
                Debug.LogError("Assegnazione units_base non riuscita");
            }
            if (baseScript != null)
            {
                baseScript.boardScript = casellScacch;
                baseScript.isPrefab = true;
                baseScript.posizione.posX = EnemiesPos[i];
                baseScript.upgs = upgs;
                baseScript.isEnemy = true;
            }

            yield return null;

        }

        yield return new WaitForSeconds(7);
        StartCoroutine(newWave());

        coroutineRunning = false;
        yield return null;

    }

    List<int> enemyType = new List<int>();


    List<int> enemySelection()
    {
        int maxTierUn=maxTierUnit;
        while (usableValue > 0)
        {
            int selected;
            if (usableValue == 1)
            {
                selected = 1;
                usableValue = 0;
                Debug.Log("selected: 1 , value 0");
            }
            else
            {

                selected = random.Next(1, usableValue + 1);
                selected = Mathf.Min(selected, maxTierUnit);

                usableValue -= selected;
                Debug.Log("selected:" + selected);
                Debug.Log("value:" + usableValue);
            }
            enemyType.Add(selected);
            if (selected == 7 || selected == 8) maxTierUnit--;
        }
        maxTierUnit= maxTierUn;
        return enemyType;


    }
    void maxValue()
    {
        if (wave == 1)
        {
            maxTierUnit = 2;
            
        }
        if (wave == 4)
        {
            maxTierUnit = 3;
            
        }
        if (wave == 8)
        {
            maxTierUnit = 4;
            maxWave = 4;
            newRows[0].SetActive(true);

        }
        if (wave == 12)
        {
            maxTierUnit = 5;
            
        }
        if (wave == 15)
        {
            maxWave = 5;
            newRows[1].SetActive(true);
        }
        if (wave == 20)
        {
            maxTierUnit = 7;
            
        }
        if (wave == 25)
        {
            maxTierUnit = 8;
            maxWave = 6;
            newRows[2].SetActive(true);
        }
    }
    IEnumerator newWave()
    {
        wave += 1;
        if (wave%3 == 0)
        {
            //Debug.Log("wtf");
            StartCoroutine(upgs.upgGenerator());
            yield return new WaitForSeconds(1);
            while (upgs.upgrading)
            {
                yield return null;
            }
        }
        EnemiesPos.Clear();
        enemiesTypeList.Clear();
        usedNumbers.Clear();
        TransformPos.Clear();
        enemyType.Clear();
        
        Debug.Log("wave:" + wave);
        float usableWave = wave;
        value = Mathf.Pow((usableWave + 1.0f) / 2.0f, 3.0f / 2.0f) + 1;
        usableValue = Mathf.RoundToInt(value);
        Debug.Log("StartingValue:" + usableValue);
        maxValue();
        yield return null;



    }

    List<Transform> intToTransform(List<int> a)
    {

        List<Transform> list = new List<Transform>();
        for (int i = 0; i < EnemiesPos.Count; i++)
        {
            switch (a[i])
            {
                case 0:
                    list.Add(firstRow[0]);
                    break;
                case 1:
                    list.Add(firstRow[1]);
                    break;
                case 2:
                    list.Add(firstRow[2]);
                    break;
                case 3:
                    list.Add(firstRow[3]);
                    break;
                case 4:
                    list.Add(firstRow[4]);
                    break;
                case 5:
                    list.Add(firstRow[5]);
                    break;


                default:
                    Debug.Log("Non genero un cazzo");
                    break;
            }
            //list.Add(firstRow[i]);
        }
        return list;
    }


    public List<int> GetRandomNumbers(int enemyNum)
    {



        while (randomNumbers.Count < enemyNum)
        {
            int randomNumber = random.Next(0, 6);
            while (usedNumbers.Contains(randomNumber))
            {
                randomNumber = random.Next(0, 6);
            }
            usedNumbers.Add(randomNumber);
            randomNumbers.Add(randomNumber);
            if (usedNumbers.Count == 6)
            {
                usedNumbers.Clear();
            }
        }

        return randomNumbers;
    }


}








