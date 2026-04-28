using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GenerateurEnnemis : MonoBehaviour
{

    public List<GameObject> listeEnnemis;

    public List<Color> listeCouleurs;

    public List<string> listeNoms;

    public List<AudioClip> listeSons;

    public float delaiGeneration = 2.5f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // GameObject ennemi1 = listeEnnemis[0];
        // GameObject dernierElement = listeEnnemis[listeEnnemis.Count - 1];
        InvokeRepeating("GenererEnnemiAleatoire",0,delaiGeneration);

        List<GameObject> listeLave = GameObject.FindGameObjectsWithTag("Lava").ToList();

        for (int i = 0; i < listeLave.Count; i++) 
        {
            listeLave[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenererEnnemiAleatoire()
    {
        // int positionAleatoire = Random.Range(0,listeEnnemis.Count);
        // GameObject ennemisModele = listeEnnemis[positionAleatoire];


        if(listeEnnemis.Count != 0)
        {
        GameObject ennemisModele = listeEnnemis[0];
        listeEnnemis.RemoveAt(0);
         GameObject clone = Instantiate(ennemisModele,transform.position,transform.rotation); 
         clone.GetComponent<Rigidbody2D>().linearVelocityX = -4;
        }
        else
        {
            CancelInvoke("GenererEnnemiAleatoire",0,delaiGeneration);
        }
    }
}
