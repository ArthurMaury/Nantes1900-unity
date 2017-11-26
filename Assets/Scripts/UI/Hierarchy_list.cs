using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hierarchy_list : MonoBehaviour {


    private ReadObjects readObjects;
    private List<GameObject> allObjects = new List<GameObject>();

    public GameObject bouton_liste;
    private Vector2 pos = Vector2.zero;

    public Color col;

    public GameObject ObjectParent;


    void Start()
    {

        readObjects = GameObject.Find("CSV_objets_lecture").GetComponent<ReadObjects>();
        allObjects = readObjects.allObjects;

        for (var i = 0; i < allObjects.Count; i++)
        {
            float yPos = i * 35;
            pos = new Vector3(0, -yPos);

            GameObject bouton = Instantiate(bouton_liste);
            bouton.name = "bouton_" + allObjects[i].name;
            bouton.transform.SetParent(ObjectParent.transform);
            bouton.GetComponent<RectTransform>().anchoredPosition = pos;

            Text bouton_texte = bouton.GetComponentInChildren<Text>();
            ObjectsInformations infos = allObjects[i].GetComponent<ObjectsInformations>();

            bouton.GetComponent<bouton_hierarchyList>().object_linked = allObjects[i];

            bouton_texte.text = infos.title;

            if (i % 2 == 0)
                bouton.GetComponent<Image>().color = col;



            Canvas.ForceUpdateCanvases();
        }


    }
}
