using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Active_object : MonoBehaviour {

	private string nom;
	public GameObject active;
	private GameObject previous,obj_zone_act,obj_zone_pre;
	private List<GameObject> SelectedObj = new List<GameObject> ();
	public Material act_mat,act_zone_mat;
	private Material cur_mat,pre_mat,pre_zone_mat,cur_zone_mat;
	private bool begin = false;

	//Les infos et label de l'objet
	private Label_UI label_script;
	private ObjectsInformations infos;

	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("text_title").GetComponent<Text> ().text != "Titre") {//Si on a un élément actif...

			nom = "object" + GameObject.Find ("texte_id").GetComponent<Text> ().text; //...On récupère son nom.

			if (nom != active.name) {//Si le nom est différent de celui de l'objet actif actuel, on va mettre à jour
				setActive ();
			}
		}
	}

	public void setActive(){ //Enregistre les caractéristiques de l'objet actif et les modifie pour le mettre en valeur

		//On récupère l'objet actif à partir de son nom
		active = GameObject.Find (nom);

		//On stocke son matériau d'origine puis on lui donne un matériau de mise en valeur
		cur_mat = active.GetComponent<MeshRenderer> ().material;
		active.GetComponent<MeshRenderer> ().material = act_mat;

		//On récupère les informations attachées à l'objet actif
		infos = active.GetComponent<ObjectsInformations> ();

		//Si l'objet actif possède une zone associée...
		if (infos.geometry_mock_up != "") {
			obj_zone_act = GameObject.Find (active.name + "_zone"); //On récupère l'objet zone

			// Si la zone est actuellement mise en valeur, son matériau d'origine est stockée dans l'attribut start_mat de "ZoneOnClick".
			if (obj_zone_act.GetComponent<ZoneOnClick> ().highlighted) { 
				cur_zone_mat = obj_zone_act.GetComponent<ZoneOnClick> ().start_mat; 
			}

			// Sinon, on récupère tout simplement le matériau sur l'objet zone.
			else { 
				cur_zone_mat = obj_zone_act.GetComponent<MeshRenderer> ().material; 
			}

			// Gestion de la zone associée à l'objet actif
			highlight_Zone ();

		}

		//Si on a un objet précédent, on va lui redonner ses caractéristiques d'origine et effacer son label
		if (begin) {
			
			//On redonne à l'objet précédent son matériau d'origine
			previous.GetComponent<MeshRenderer> ().material = pre_mat;

			//On récupère les informations attachées à l'objet précédent
			infos = previous.GetComponent<ObjectsInformations> ();

			//On efface le label de l'objet précédent sauf s'il appartient à la liste d'objets sélectionnés
			SelectedObj = GameObject.Find ("Documents").GetComponent<ObjectSelection> ().SelectedObj;

			if (!SelectedObj.Contains (previous)) {
				label_script = infos.label.GetComponent<Label_UI> ();
				label_script.label_pointe_RT.localScale = Vector3.zero;
				label_script.isVisible = false;
			}

			//Si l'objet précédent possède une zone associée, on redonne à cette zone son matériau d'origine
			if (infos.geometry_mock_up != "") {
				obj_zone_pre.GetComponent<MeshRenderer> ().material = pre_zone_mat; // On modifie son matériau
			}
		}
			
		//On passe les paramètres de l'objet actif dans l'objet précédent
		previous = active;
		obj_zone_pre = obj_zone_act;
		pre_mat = cur_mat;
		pre_zone_mat = cur_zone_mat;

		//On enregistre le fait qu'on a un objet précédent
		begin = true;
	}

	public void highlight_Zone(){

		// Si toutes les zones sont affichées, on met en valeur celle associée à l'objet actif
		if (GameObject.Find ("Button_afficherZones").GetComponent<ShowZones> ().visible) {
			obj_zone_act.GetComponent<MeshRenderer> ().material = act_zone_mat;
		} 

		//Sinon, on redonne à la zone son matériau d'origine et on l'affiche
		else {
			obj_zone_act.GetComponent<MeshRenderer> ().material = cur_zone_mat;
			obj_zone_act.transform.localScale = Vector3.one;
		}
	}
}
