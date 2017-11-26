using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ObjectSelection : MonoBehaviour {

	// Attributs utilisés pour la sélection 
	private Camera mainCam;
	private bool click=false;
	private Vector2 posOrigin, posFinale;
	private Rect viewport_rect;

	// Attributs utilisés pour la création de la liste des objects sélectionnés
	public GameObject bouton_liste;
	public GameObject ObjectParent;
	public List<GameObject> SelectedObj = new List<GameObject> ();
	public List<GameObject> SelectedButton = new List<GameObject>();
	private List<GameObject> allObjects = new List<GameObject>();
	private RectTransform Cont_rect, Doc_rect;

	// le label et les informations associés à l'objet
	private Label_UI label_script;
	private ObjectsInformations infos;


	void Start () {

		//Au lancement, on récupère tous les éléments nécessaires :
		mainCam = GameObject.Find("Main Camera").GetComponent<Camera>(); //La caméra prinicpale
		allObjects = GameObject.Find("CSV_objets_lecture").GetComponent<ReadObjects> ().allObjects; //La liste de tous les objets
		//Les RectTransform des 2 éléments du menu des objets sélectionnés
		Cont_rect = GameObject.Find ("Content").GetComponent<RectTransform>();
		Doc_rect = GameObject.Find ("Documents").GetComponent<RectTransform>();

		//On crée un rectangle qui va permettre de déterminer si la souris est bien sur le viewport
		viewport_rect = new Rect (0,0, mainCam.rect.width*Screen.width, mainCam.rect.height*Screen.height);
	}
		
	void Update () {

		//Si le bouton gauche de la souris est enfoncé et qu'elle se situe dans le viewport, on va enregistrer sa position
		if ((Input.GetMouseButtonDown (0)) && (viewport_rect.Contains(Input.mousePosition))) {
			
			click = true;
			posOrigin = Input.mousePosition;
		}

		//Si la souris est dans le viewport et que l'on relache le bouton gauche, on enregistre une nouvelle position
		if ((click) && (Input.GetMouseButtonUp (0)) && (viewport_rect.Contains(Input.mousePosition))) {

			posFinale = Input.mousePosition;

			//Si la position de départ et la position finale de la souris au cours d'un clic sont différentes (c'est à dire si on a dessiné un rectangle)...
			if (posFinale != posOrigin) {
				
				//Si on avait déjà une liste d'objets sélectionnés, on la vide et on cache les labels correspondants.
				if (SelectedButton.Count >= 1) {
					Redim ();
					ClearLabels ();
				}

				//On lance la création d'une nouvelle liste d'objets sélectionnés et on l'affiche
				selectMultipleObject (posOrigin, posFinale);
				DisplayList ();
			}
		}
	}

	public void selectMultipleObject (Vector2 a, Vector2 b) //Cette fonction permet de créer une liste d'objet sélectionnés
	{

		//On va tester pour tous les objets s'ils sont contenus dans le rectangle qu'on vient de dessiner avec la souris
		foreach (GameObject go in allObjects)
		{
			var screenCoord = mainCam.WorldToScreenPoint (go.transform.position); //On ramène les coordonnées 3D de l'objet dans le repère 2D de l'écran 

			Rect selectionBox = new Rect (Mathf.Min (a.x, b.x), Mathf.Min (a.y, b.y), Mathf.Abs (a.x - b.x), Mathf.Abs (a.y - b.y)); //On dessine notre rectangle de sélection

			if (selectionBox.Contains(screenCoord)){

				SelectedObj.Add(go); //Si l'objet est contenu dans le rectangle de sélection on l'ajoute à la liste des objets sélectionnés.
			}
		}
	}

	public void DisplayList() //Cette fonction crée une liste de boutons et l'affiche dans la section dédiée du panneau latéral droit. Elle permet également l'affichage des labels des objets sélectionnés.
	{

		for (var i = 0; i < SelectedObj.Count; i++)
		{
			//Création des boutons de la liste d'objects sélectionnés
			float yPos = i * 25;
			Vector3 pos = new Vector3(1, -yPos);

			GameObject bouton = Instantiate(bouton_liste);
			bouton.name = "bouton_" + SelectedObj[i].name;
			bouton.transform.SetParent(ObjectParent.transform);
			bouton.GetComponent<RectTransform>().anchoredPosition = pos;
			bouton.GetComponent<RectTransform>().sizeDelta = new Vector2 (370, 25);

			Text bouton_texte = bouton.GetComponentInChildren<Text>();
			ObjectsInformations infos = SelectedObj[i].GetComponent<ObjectsInformations>();

			bouton.GetComponent<bouton_hierarchyList>().object_linked = SelectedObj[i];

			bouton_texte.text = infos.title;

			if (i % 2 == 0)
				bouton.GetComponent<Image> ().color = new Vector4 (1, 1, 1, 1);



			//Si nécessaire, on agrandit le menu des objets sélectionnés 
			if (i > 10) {
				Vector2 dec = new Vector2 (0, 25);

				Cont_rect.sizeDelta += dec;
				Doc_rect.sizeDelta += dec;
			}

			//On force la mise à jour des canevas
			Canvas.ForceUpdateCanvases ();

			//On place les boutons créés dans une liste
			SelectedButton.Add (bouton);

			//Affichage des labels des objets sélectionnés
			label_script = infos.label.GetComponent<Label_UI> ();
			label_script.label_pointe_RT.localScale = Vector3.one;
			label_script.isVisible = true;

		}
			
	}

	public void ClearLabels() //Cette fonction permet d'effacer les labels sur la maquette et de vider la liste des objets sélectionnés.
	{
			
		foreach (GameObject go in SelectedObj) {

			//On efface les labels de tous les objets sélectionnés sauf si l'un d'entre eux est l'objet actif
			if (go != GameObject.Find ("Active_object").GetComponent<Active_object> ().active) {
				infos = go.GetComponent<ObjectsInformations> ();
				label_script = infos.label.GetComponent<Label_UI> ();
				label_script.label_pointe_RT.localScale = Vector3.zero;
				label_script.isVisible = false;
			}
		}
		SelectedObj.Clear (); //On vide la liste des objets sélectionnés
	}

	public void Redim(){ //Cette fonction détruit les boutons et ré-initialise le panneau latéral droit
		
		//On détruit les boutons existants
		foreach (GameObject go in SelectedButton) {
			Destroy (go);
		}

		//On vide la liste des boutons
		SelectedButton.Clear ();

		//On ré-initialise la section du panneau latéral droit à ses valeurs d'origine
		Cont_rect.sizeDelta = new Vector2 (470, 850);
		Doc_rect.sizeDelta = new Vector2 (443, 300);
	}
}
