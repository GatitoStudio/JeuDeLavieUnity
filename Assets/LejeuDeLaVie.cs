using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LejeuDeLaVie : MonoBehaviour
{
    public GameObject GoAInstancier;
    public GameObject[,] lesgo;
    public int[,] lescellules;
    public int[,] lescellulesApres;

    public int taille;
    public float attenteEntreEtape = 1;
    public int nbEtape = 100;
    public int nbrPopulation = 214;
    // Start is called before the first frame update
    void Start()
    {
        lescellules = new int[taille, taille];
        lescellulesApres = new int[taille, taille];

        lesgo = new GameObject[taille, taille];
        LeJeu();
        StartCoroutine(PUATIN());
    }

    public void LeJeu()
    {
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                GameObject go = Instantiate(GoAInstancier, new Vector3(i, j, 0), Quaternion.identity);
                lesgo[i, j] = go;
                lescellules[i, j] = 0;
                lescellulesApres[i, j] = 0;
            }
        }
        for (int i = 0; i < nbrPopulation; i++)
        {
            int x = Random.Range(0, taille);
            int y = Random.Range(0, taille);
            while (lescellules[x, y] != 0)
            {
                x = Random.Range(0, taille);
                y = Random.Range(0, taille);
            }

            lescellules[x, y] = 1;
            lesgo[x, y].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
        //lescellules[5, 5] = 1;
        //lesgo[5, 5].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        //lescellules[6, 5] = 1;
        //lesgo[6, 5].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        //lescellules[7, 5] = 1;
        //lesgo[7, 5].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }
    IEnumerator PUATIN()
    {
        while (true)
        {
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    int NbrVoisin = 0;
                    int etat = lescellules[x, y];
                    if (x - 1 > -1)
                    {
                        if (lescellules[x - 1, y] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (x + 1 < taille)
                    {
                        if (lescellules[x + 1, y] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (y - 1 > -1)
                    {
                        if (lescellules[x, y - 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (y + 1 < taille)
                    {
                        if (lescellules[x, y + 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (x - 1 > -1 && y - 1 > -1)
                    {
                        if (lescellules[x - 1, y - 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (x + 1 < taille && y + 1 < taille)
                    {
                        if (lescellules[x + 1, y + 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (x - 1 > -1 && y + 1 < taille)
                    {
                        if (lescellules[x - 1, y + 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (x + 1 < taille && y - 1 > -1)
                    {
                        if (lescellules[x + 1, y - 1] == 1)
                        {
                            NbrVoisin++;
                        }
                    }
                    if (etat == 1)
                    {
                        if (NbrVoisin != 3 && NbrVoisin != 2)
                        {
                            lescellulesApres[x, y] = 0;
                        }
                        else
                        {
                            lescellulesApres[x, y] = 1;
                        }
                    }
                    else
                    {
                        if (NbrVoisin == 3)
                        {
                            lescellulesApres[x, y] = 1;
                        }
                    }
                    Debug.Log(NbrVoisin);
                }

            }
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    if(lescellulesApres[x, y]==1)
                        lesgo[x, y].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                    else
                        lesgo[x, y].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                    lescellules[x, y] = lescellulesApres[x, y];           
                }
            }
            yield return new WaitForSeconds(attenteEntreEtape);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
