using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bouton_hierarchyList : MonoBehaviour {


    public GameObject object_linked;
    private ObjectsInformations infos;

	//les détails à modifier dans le panneau_details
	private GameObject details;

	private bool visible = false;

    public Text title_field;
    public Text description_field;

    void Start () {

        infos = object_linked.GetComponent<ObjectsInformations>();

        title_field = GameObject.Find("text_title").GetComponent<Text>();
        description_field = GameObject.Find("text_description").GetComponent<Text>();
    }
	

	void Update () {

        
	
	}


    public void Onclick()
    {
        title_field.text = infos.title;
        description_field.text = infos.description;

		visible = GameObject.Find ("Button_afficherZones").GetComponent<ShowZones> ().visible;

		if (!visible) {
			GameObject[] zones = GameObject.FindGameObjectsWithTag ("Zone");
			foreach (GameObject go in zones) {
				go.transform.localScale = Vector3.zero;
			}

			if (infos.geometry_mock_up != "") {
				GameObject obj_zone = GameObject.Find (object_linked.name + "_zone");
				obj_zone.transform.localScale = Vector3.one;
			}
		}

		//On met à jour les informations du bandeau details
		details = GameObject.Find ("texte_version");
		details.GetComponent<Text> ().text = infos.version;
		details = GameObject.Find ("texte_id");
		details.GetComponent<Text> ().text = infos.id;
		details = GameObject.Find ("texte_statut");
		details.GetComponent<Text> ().text = infos.status;
    }
}
