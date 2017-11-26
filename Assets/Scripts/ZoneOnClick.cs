using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneOnClick : MonoBehaviour {

	private Material act_mat;
	private string nom;
	private char[] Char = new char[5];
	private bool visible =false;
	private RectTransform opt_rect;
	private Rect optionBox;

	public bool highlighted=false;
	public Material start_mat;


	void Start(){
		//On récupère le matériau qui sert à mettre la zone en valeur
		act_mat = GameObject.Find ("Active_object").GetComponent<Active_object> ().act_zone_mat;

		//On crée le tableau de caractère qui va être utilisé par la fonction TrimEnd
		Char [0] = '_';
		Char [1] = 'z';
		Char [2] = 'o';
		Char [3] = 'n';
		Char [4] = 'e';

		//On crée un rectangle au niveau de la boite des options d'affichage
		opt_rect = GameObject.Find ("Panneau_options-affichage").GetComponent<RectTransform> ();
		optionBox = new Rect (opt_rect.position.x - opt_rect.sizeDelta.x / 2, opt_rect.position.y - opt_rect.sizeDelta.y / 2, opt_rect.sizeDelta.x, opt_rect.sizeDelta.y);
	}

	void OnMouseEnter(){
		//Lorsque la souris entre dans la zone, on échange son matériau avec celui de mise en valeur 
		start_mat = gameObject.GetComponent<MeshRenderer>().material;
		gameObject.GetComponent<MeshRenderer> ().material = act_mat;
		highlighted = true;
	}

	void OnMouseOver(){
		//Lorsque la souris est dans une zone et qu'on clique hors de la boite des options d'affichage, on récupère l'objet associé à la zone et on affiche ses informations ainsi que son label.
		if (Input.GetMouseButtonDown(0)&&(!optionBox.Contains(Input.mousePosition))){ 

			//A partir du nom de la zone "objectXX_zone" on récupère le nom de l'objet associé "objectXX" grâce à la fonction TrimEnd
			nom = gameObject.name;
			nom = nom.TrimEnd (Char); 

			//Si la touche MAJ est enfoncée au moment du clic, on ajoute l'objet associé à la zone dans la liste des objets sélectionnés
			if (Input.GetKey (KeyCode.LeftShift)) {
				GameObject.Find (nom).GetComponent<ObjetOnClick> ().AddToList ();
			} 

			//On appelle la fonction utilisée lorsque'on clique sur un objet
			GameObject.Find (nom).GetComponent<ObjetOnClick> ().DisplayLabel ();
		}


	}

	void OnMouseExit(){
		visible = GameObject.Find ("Button_afficherZones").GetComponent<ShowZones> ().visible;

		//Si les zones ne sont pas affichées, on redonne à la zone son matériau d'origine
		if (!visible) {
			gameObject.GetComponent<MeshRenderer> ().material = start_mat;
		}

		//Si toutes les zones sont affichées et que la souris sort d'une zone, on lui redonne son matériau d'origine que si l'objet n'est pas actif
		else {
			if (GameObject.Find ("Active_object").GetComponent<Active_object> ().active.name != nom) {
				gameObject.GetComponent<MeshRenderer> ().material = start_mat;
			}
		}
		highlighted = false;
	}
		
}
