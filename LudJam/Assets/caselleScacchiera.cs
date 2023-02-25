using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Windows;

public class caselleScacchiera : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    casella[][] ArrayScacchiera = new casella[6][];
    scacchiera scacc;
    Transform transform2;
    private void Awake()
    {
        scacc=GetComponent<scacchiera>();
        transform2 = transform;
        ArrayScacchiera = new casella[][] {
            new casella[] {
                new casella("A0", false, "", null, 0, 0, 100),                   //Ally
                new casella("A1", false, "", null, 0, 1, 100),                   //Ally
                new casella("A2", false, "", null, 0, 2, 100),                   //Ally
                new casella("A3", false, "", null, 0, 3, 100),                   //Ally
                new casella("A4", false, "", null, 0, 4, 100),                   //Ally
                new casella("A5", false, "", null, 0, 5, 100),                   //Ally
            },                                                                   //Ally
            new casella[] {                                                      //Ally
                new casella("B0", false, "", null, 1, 0, 100),                   //Ally
                new casella("B1", false, "", null, 1, 1, 100),                   //Ally
                new casella("B2", false, "", null, 1, 2, 100),                   //Ally
                new casella("B3", false, "", null, 1, 3, 100),                   //Ally
                new casella("B4", false, "", null, 1, 4, 100),                   //Ally
                new casella("B5", false, "", null, 1, 5, 100),                   //Ally
            },                                                                   //Ally
            new casella[] {                                                      //Ally
                new casella("C0", false, "", null, 2, 0, 100),                   //Ally
                new casella("C1", false, "", null, 2, 1, 100),                   //Ally
                new casella("C2", false, "", null, 2, 2, 100),                   //Ally
                new casella("C3", false, "", null, 2, 3, 100),                   //Ally
                new casella("C4", false, "", null, 2, 4, 100),                   //Ally
                new casella("C5", false, "", null, 2, 5, 100),                   //Ally
            },
            new casella[] {
                new casella("D0", false, "", null, 3, 0, 100),
                new casella("D1", false, "", null, 3, 1, 100),
                new casella("D2", false, "", null, 3, 2, 100),
                new casella("D3", false, "", null, 3, 3, 100),
                new casella("D4", false, "", null, 3, 4, 100),
                new casella("D5", false, "", null, 3, 5, 100),
            },
            new casella[] {
                new casella("E0", false, "", null, 4, 0, 100),
                new casella("E1", false, "", null, 4, 1, 100),
                new casella("E2", false, "", null, 4, 2, 100),
                new casella("E3", false, "", null, 4, 3, 100),
                new casella("E4", false, "", null, 4, 4, 100),
                new casella("E5", false, "", null, 4, 5, 100),
            },
            new casella[] {
                new casella("F0", false, "", null, 5, 0, 100),
                new casella("F1", false, "", null, 5, 1, 100),
                new casella("F2", false, "", null, 5, 2, 100),
                new casella("F3", false, "", null, 5, 3, 100),
                new casella("F4", false, "", null, 5, 4, 100),
                new casella("F5", false, "", null, 5, 5, 100),
            },
            new casella[] {
                new casella("G0", false, "", null, 6, 0, 100),
                new casella("G1", false, "", null, 6, 1, 100),
                new casella("G2", false, "", null, 6, 2, 100),
                new casella("G3", false, "", null, 6, 3, 100),
                new casella("G4", false, "", null, 6, 4, 100),
                new casella("G5", false, "", null, 6, 5, 100),
            },
            new casella[] {
                new casella("H0", false, "", null, 7, 0, 100),
                new casella("H1", false, "", null, 7, 1, 100),
                new casella("H2", false, "", null, 7, 2, 100),
                new casella("H3", false, "", null, 7, 3, 100),
                new casella("H4", false, "", null, 7, 4, 100),
                new casella("H5", false, "", null, 7, 5, 100),
            },
        };
    }
    public IEnumerator caselleAllies(string f, Vector3 ps)
    {
        Debug.Log("3");
        int y;
        int x;
        
        y = int.Parse(f.Substring(1));
        x = f[0] - 'A';
        if (ArrayScacchiera[y][x].full == false && x<scacc.maxWave)
        {
            ArrayScacchiera[y][x].full = true;
            Debug.Log("4");
            playerManager.spawning(ps, x,y);
        }else yield return null;
    }
    public void freeCasella(int x, int y)
    {
        ArrayScacchiera[y][x].full = false;
    }
    public casella syncCasella(int x, int y, bool spawn=false)
    {
        casella requested = ArrayScacchiera[y][x];
        requested.position = transform.Find(requested.pos).gameObject.transform;
        
        if (requested.position != null)
        {
            return requested;
        }
        else
        {
            Debug.LogError("SyncNonRiuscito");
            return null;

        }
        
    }

}
public class casella
{
    public string pos;
    public bool full;
    public string unit;
    public Transform position;
    public int posX;
    public int posY;
    public int hp;
    public casella(string a = "", bool b = false, string c = "", Transform d = null, int e = 0, int f = 0, int g = 100)
    {
        this.pos = a;
        this.full = b;
        this.unit = c;
        this.position = d;
        this.posX = e;
        this.posY = f;
        this.hp = g;
    }
}
