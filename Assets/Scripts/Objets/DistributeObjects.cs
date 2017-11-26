using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DistributeObjects : MonoBehaviour {


    private ReadObjects readObjects;

    private Vector3 pos = Vector3.zero;
    public float coordonates_Multiplicater = 1;
    public float offsetX = 0;
    public float offsetZ = 0;

    private float coordonates_Multiplicater_check = 1;
    private float offsetX_check = 0;
    private float offsetZ_check = 0;
    private bool valueChanged = false;

    private List<GameObject> allObjects = new List<GameObject>();
    private List<GameObject> spatializedObjects = new List<GameObject>();
    private List<GameObject> noSpatializedObjects = new List<GameObject>();

    public Material spatialized_mat;
    public Material noSpatialized_mat;

    public GameObject emptyObject;
	public bool ZoneList =false;


    void Start () {


        readObjects = GameObject.Find("CSV_objets_lecture").GetComponent<ReadObjects>();

        allObjects = readObjects.allObjects;
        spatializedObjects = readObjects.spatializedObjects;
        noSpatializedObjects = readObjects.noSpatializedObjects;

        PlacerLesObjets();

        offsetX_check = offsetX;
        offsetZ_check = offsetZ;
        coordonates_Multiplicater_check = coordonates_Multiplicater;


    }

	
    void Update()
    {

        //////ça c'est pour faire en sorte que lorsque l'on modifie les valeurs dans l'inspecteur, l'affichage in-game s'actualise
        if(offsetX_check != offsetX || offsetZ_check != offsetZ || coordonates_Multiplicater_check != coordonates_Multiplicater)
        {
            PlacerLesObjets();
            offsetX_check = offsetX;
            offsetZ_check = offsetZ;
            coordonates_Multiplicater_check = coordonates_Multiplicater;
        }

        

    }


    private void PlacerLesObjets()
    {
        ///////////Placer les objets...////////////

        ///...spatialisés...///
        for (var i = 0; i < spatializedObjects.Count; i++)
        {
            GameObject obj = spatializedObjects[i];
            ObjectsInformations infos = obj.GetComponent<ObjectsInformations>();
            obj.GetComponent<Renderer>().material = spatialized_mat;

            pos = infos.GPS_converted * coordonates_Multiplicater - new Vector3(offsetX, -8.70f, offsetZ);


            /////On vérifie qu'aucun objet n'est présent à la position ciblée

            Collider[] hitColliders = Physics.OverlapSphere(pos, 0.51f);
            
            if(hitColliders.Length > 0) { 
               // print(obj.name + " : " + hitColliders.Length);   
                
                               
                pos += new Vector3(0, 1.3f * hitColliders.Length, 0);
                obj.transform.position = pos;
            }

            else if (hitColliders.Length == 0)
            {
                obj.transform.position = pos;
            }
           



            

           


            ///////On ajuste la hauteur par rapport au maillage de la maquette

            //float high = 10;
            //RaycastHit hit;
            //if (Physics.Raycast(obj.transform.position, -obj.transform.up, out hit, 500.0f))
            //{
            //    print(hit.transform.name);
            //    high = hit.point.y + 1;
            //    print(obj.transform.position);
            //    obj.transform.position = new Vector3(obj.transform.position.x, high, obj.transform.position.z);
            //}



        }

        ///...et non spatializés///

        for (var i = 0; i < noSpatializedObjects.Count; i++)
        {
            GameObject obj = noSpatializedObjects[i];
            ObjectsInformations infos = obj.GetComponent<ObjectsInformations>();
            Vector3 sumPosition = new Vector3(0, 0, 0);
            Vector3 averagePosition;

            obj.GetComponent<Renderer>().material = noSpatialized_mat;

            if (infos.objets_en_relation.Count > 1)
            {
                for (var j = 0; j < infos.objets_en_relation.Count; j++)
                {
                    sumPosition += infos.objets_en_relation[j].transform.position;
                }

                averagePosition = sumPosition / infos.objets_en_relation.Count;
                obj.transform.position = new Vector3(averagePosition.x, 25, averagePosition.y);
            }
            else
                obj.transform.position = new Vector3(0, 0, 0);


            
        }


        /////////On crée des objets enfants, pour accueillir les mesh de zone
        for(var i = 0; i < allObjects.Count; i++)
        {
            GameObject obj = allObjects[i];
            ObjectsInformations infos = obj.GetComponent<ObjectsInformations>();

            if (infos.geometry_mock_up != "")
            {
                GameObject emptyChild = Instantiate(emptyObject) as GameObject;
                emptyChild.name = obj.name + "_zone";
                emptyChild.transform.SetParent(GameObject.Find("Zones").transform);
                emptyChild.layer = 13;
                emptyChild.tag = "Zone";
                emptyChild.transform.localScale = Vector3.zero;
            }


        }
		ZoneList = true; 

    }


}
