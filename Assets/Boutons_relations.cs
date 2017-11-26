using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Boutons_relations : MonoBehaviour {

	private GameObject actif;
	private GameObject previous;
	public GameObject bouton_liste;
	public GameObject ObjectParent;
	private RectTransform Pan_details_rect;
	public List<GameObject> relations = new List<GameObject>();
	private List<GameObject> RelatList = new List<GameObject>();
	private Vector2 size_init, pos_init;

	// Use this for initialization
	void Start () {
		Pan_details_rect = GameObject.Find ("Panneau_details").GetComponent<RectTransform>();
		pos_init = Pan_details_rect.anchoredPosition;
		size_init = Pan_details_rect.sizeDelta;

	}
	
	// Update is called once per frame
	void Update () {

		//Si on a un objet actif on va modifier le panneau détail
		if (GameObject.Find ("text_title").GetComponent<Text> ().text != "Titre") {

			Vector2 dec = new Vector2 (0,22); //Vecteur utile pour le redimensionnement du panneau détail

			//On récupère l'objet actif et ses objets en relations
			actif = GameObject.Find ("Active_object").GetComponent<Active_object> ().active;
			relations = actif.GetComponent<ObjectsInformations> ().objets_en_relation;

			//Si on change d'objet actif...
			if (previous != actif) {

				//On revient à la définition initiale du panneau détails
				Pan_details_rect.sizeDelta = size_init;
				Pan_details_rect.anchoredPosition = pos_init;

				//On détruit les boutons relations existants
				foreach (GameObject go in RelatList) {
					Destroy (go);
				}

				//S'il existe des éléments en relation...
				if (relations.Count > 0) {

					//On retire le texte sous la section "Objets en relations"
					GameObject.Find ("texte_relations").GetComponent<Text> ().text = "";

					//Création des boutons pour chaque objet en relation
					for (var i = 0; i < relations.Count; i++) {
					
						//Permet de positionner le bouton
						float yPos = i * 24;
						Vector3 pos = new Vector3 (-16, -yPos);

						//On instantie un prefab de bouton et on paramètre toutes ses caractéristiques
						GameObject bouton = Instantiate (bouton_liste);
						bouton.name = "bouton_relation_" + relations [i].GetComponent<ObjectsInformations> ().id; //nom
						bouton.transform.SetParent (ObjectParent.transform); //objet parent sur le canevas
						bouton.GetComponent<RectTransform> ().anchoredPosition = pos; //position
						bouton.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1); //échelle 
						bouton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (504, 24); //taille
						bouton.GetComponent<Image> ().color = new Vector4 (1, 1, 1, 1); //couleur du fond (blanc)
						bouton.GetComponent<bouton_hierarchyList> ().object_linked = relations [i]; //élément pointé par le bouton

						//Texte affiché sur le bouton
						Text bouton_texte = bouton.GetComponentInChildren<Text> ();
						ObjectsInformations infos = relations [i].GetComponent<ObjectsInformations> ();
						bouton_texte.text = infos.title;

						// On change la couleur des boutons impairs
						if (i % 2 == 1) {
							bouton.GetComponent<Image> ().color = new Vector4 (0.8f, 0.8f, 0.8f, 1);
						}

						//Agrandissement du panneau détail pour insérer les boutons (seulement s'il y a plus de 1 bouton)
						if (i != 0) {
							Pan_details_rect.sizeDelta += dec;
							Pan_details_rect.anchoredPosition -= dec;
						}
							
						//On ajoute le boutons à la liste RelatListe
						RelatList.Add (bouton);

						//On force la mise à jour du canevas
						Canvas.ForceUpdateCanvases ();
					}


				}

				//Si on a pas d'objets en relation avec l'objet actif, on modifie le texte de la section en conséquence
				else {
					GameObject.Find ("texte_relations").GetComponent<Text> ().text = "Pas d'éléments en relation";
				}
			} 

			previous = actif;
		}
	}
}
