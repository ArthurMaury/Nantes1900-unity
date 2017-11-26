using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Au lancement de l'appli, création de tout les labels
public class CreateLabels : MonoBehaviour {

    //Le prefab à utiliser
    public GameObject label_prefab;

    private ReadObjects readObjects;
    public List<GameObject> allObjects = new List<GameObject>();


    void Start () {

        readObjects = GameObject.Find("CSV_objets_lecture").GetComponent<ReadObjects>();
        allObjects = readObjects.allObjects;


        for (var i = 0; i < allObjects.Count; i++)
        {
            GameObject obj = allObjects[i];
            ObjectsInformations infos = obj.GetComponent<ObjectsInformations>();

            GameObject obj_label = Instantiate(label_prefab);
            obj_label.name = "label_" + obj.name;
            obj_label.tag = "Label";
            obj_label.transform.SetParent(gameObject.transform);
            obj_label.transform.position = obj.transform.position;

            obj_label.GetComponentInChildren<Text>().text = infos.title;
            obj_label.GetComponentInChildren<CameraFacingBillboard>().m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

            infos.label = obj_label;
        }

    
    }
	


}
