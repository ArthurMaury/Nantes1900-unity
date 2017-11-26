using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Présente sur chaque objet, cette class gère les clics
public class ObjetOnClick : MonoBehaviour {


    private ObjectsInformations infos;
	private List<GameObject> SelectedButton = new List<GameObject>();
	private List<GameObject> SelectedObj = new List<GameObject>();

	//les détails à modifier dans le panneau_details
	private GameObject details;

    //l'entrée titre du panneau latéral droit
    private Text title_field;
    //l'entrée description du panneau latéral droit
    private Text description_field;
    //le label associé à l'objet
    private Label_UI label_script;
	//
	private bool visible = false;


    void Start () {
    }


    void OnMouseDown()
	{

		if (Input.GetKey (KeyCode.LeftShift)) {
			AddToList ();
			DisplayLabel ();
		}
		else {
			GameObject.Find ("Documents").GetComponent<ObjectSelection> ().Redim ();
			GameObject.Find ("Documents").GetComponent<ObjectSelection> ().ClearLabels ();
			DisplayLabel ();
		}
	}

    //Active le label de l'objet, et affiche les infos dans le panneau latéral
    //À noter : lorsque l'on veut faire apparaître un label, on fait passer sa "localSize" de 0 à 1
    public void DisplayLabel()
	{
		infos = gameObject.GetComponent<ObjectsInformations> ();

		title_field = GameObject.Find ("text_title").GetComponent<Text> ();
		description_field = GameObject.Find ("text_description").GetComponent<Text> ();
		label_script = infos.label.GetComponent<Label_UI> ();

		title_field.text = infos.title;
		description_field.text = infos.description;

		visible = GameObject.Find ("Button_afficherZones").GetComponent<ShowZones> ().visible;

		if (!visible) {
			GameObject[] zones = GameObject.FindGameObjectsWithTag ("Zone");
			foreach (GameObject go in zones) {
				go.transform.localScale = Vector3.zero;
			}

			if (infos.geometry_mock_up != "") {
				GameObject obj_zone = GameObject.Find (gameObject.name + "_zone");
				obj_zone.transform.localScale = Vector3.one;
			}
		}
			
		label_script.label_pointe_RT.localScale = Vector3.one;
		label_script.isVisible = true;

		//On met à jour les informations du bandeau details
		details = GameObject.Find ("texte_version");
		details.GetComponent<Text> ().text = infos.version;
		details = GameObject.Find ("texte_id");
		details.GetComponent<Text> ().text = infos.id;
		details = GameObject.Find ("texte_statut");
		details.GetComponent<Text> ().text = infos.status;
		
	}

	public void AddToList(){ //Ajoute l'objet à la liste des objets sélectionnés

		//On récupère la liste de objets sélectionnés
		SelectedObj = GameObject.Find ("Documents").GetComponent<ObjectSelection> ().SelectedObj;

		//Si l'objet n'est pas déjà dans la liste on l'ajoute
		if (!SelectedObj.Contains (gameObject)) {
			SelectedObj.Add (gameObject);
		}
		//On ré-initialise la section dédiée du canevas et on lance la création de nouveaux boutons
		GameObject.Find ("Documents").GetComponent<ObjectSelection> ().Redim ();
		GameObject.Find ("Documents").GetComponent<ObjectSelection> ().DisplayList();

	}
}
