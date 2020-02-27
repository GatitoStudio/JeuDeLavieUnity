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
        //n-1
        lescellules = new int[taille, taille];
        //n , la prochaine configuration 
        lescellulesApres = new int[taille, taille];
        //Pour afficher 
        lesgo = new GameObject[taille, taille];

        Initialisation();
        StartCoroutine(Lejeu());
    }

    public void Initialisation()
    {
        //Je mets tout a 0 
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
        //Random Population
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
    IEnumerator Lejeu()
    {
        //C'est degueulasse j'ai fais une boucle infinie , on pourrais faire un boolean controler par un bouton 
        while (true)
        {
            //Pour chaque element du tableau (ou pour chaque population 
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    int NbrVoisin = 0;
                    int etat = lescellules[x, y];
                    //Verification des 8 potentiels voisins 
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
                    //Etat a l'iteration suivante
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
            //j'applique et j'update
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
