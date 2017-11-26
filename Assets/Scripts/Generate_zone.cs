using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Générer la zone de sélection suite à un clic gauche
public class Generate_zone : MonoBehaviour {

    private Camera mainCam;

    public GameObject labelZone_prefab;
    public Material[] zoneMaterial;

    public List<GameObject> labelZones = new List<GameObject>();
    private int index = -1;

    public float expansionSpeed;

    //Le layer utilisé pour les raycast
    //Il s'agit de faire en sorte que la collision du raycast ne s'opère pas 
    //sur le propre collider de la zone de sélection
    int layerMask;


    void Start () {

        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        layerMask = 1 << 14;
        layerMask = ~layerMask;

    }
	


	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5000.0f, layerMask))
            {
                if(hit.transform.tag != "Objet") { 
                    index++;

                    GameObject labelZone = Instantiate(labelZone_prefab);
                    labelZone.name = "LabelZone_" + index.ToString();
                    labelZone.transform.SetParent(gameObject.transform);
                    labelZones.Add(labelZone);
                    labelZone.transform.position = new Vector3(hit.point.x, 60, hit.point.z);
                   
                    int mat_index = Random.Range(0, zoneMaterial.Length);                    
                    labelZone.GetComponentInChildren<Projector>().material = zoneMaterial[mat_index];
                }
            }
        }


        if (Input.GetMouseButton(0))
        {
            
            Projector labelZone_projector = labelZones[index].GetComponentInChildren<Projector>();
            labelZone_projector.orthographicSize += (expansionSpeed);

            Transform labelZone_collider = labelZones[index].transform.GetChild(1);
            Vector3 scale = new Vector3 (labelZone_projector.orthographicSize * 1.76f, 3.3f, labelZone_projector.orthographicSize * 1.76f);
            labelZone_collider.localScale = scale;

            
        }


        if (Input.GetMouseButtonUp(0))
        {
            Label_zone zone_script = labelZones[index].GetComponent<Label_zone>();
            zone_script.SetDisplay();
        }


            if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5000.0f))
            {
                if(hit.transform.tag == "Label_zone") {

                    string label_name = hit.transform.parent.name;

                    for(var i = 0; i < labelZones.Count; i++)
                    {
                        if(labelZones[i].name == label_name)
                        {
                            index--;
                            Destroy(labelZones[i]);
                            labelZones.Remove(labelZones[i]);
                        }
                    }
                }

            }
        }



    }
}
