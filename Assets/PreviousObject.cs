using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PreviousObject : MonoBehaviour {

	private GameObject object_linked, pre, actif, details;
	private ObjectsInformations infos;
	private Text title_field;
	private Text description_field;

	void Start (){
		title_field = GameObject.Find ("text_title").GetComponent<Text> ();
		description_field = GameObject.Find ("text_description").GetComponent<Text> ();
	}

	void Update () {

		actif = GameObject.Find ("Active_object").GetComponent<Active_object> ().active;

		if ((actif!=pre)&&(pre!=null)){
			object_linked = pre; 
		}

		pre = GameObject.Find ("Active_object").GetComponent<Active_object> ().active;

	}

	public void Onclick()
	{
		if (object_linked != null) {
			infos = object_linked.GetComponent<ObjectsInformations> ();

			title_field.text = infos.title;
			description_field.text = infos.description;

			GameObject[] zones = GameObject.FindGameObjectsWithTag ("Zone");
			foreach (GameObject go in zones) {
				go.transform.localScale = Vector3.zero;
			}

			if (infos.geometry_mock_up != null && infos.geometry_mock_up != "") {
				GameObject obj_zone = GameObject.Find (object_linked.name + "_zone");
				obj_zone.transform.localScale = Vector3.one;
			}

			//Mise à jour des informations du bandeau details
			details = GameObject.Find ("texte_version");
			details.GetComponent<Text>().text = infos.version;
			details = GameObject.Find ("texte_id");
			details.GetComponent<Text>().text = infos.id;
			details = GameObject.Find ("texte_statut");
			details.GetComponent<Text>().text = infos.status;
		}
	}
}

