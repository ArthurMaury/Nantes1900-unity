using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowZones : MonoBehaviour {

	private bool ZoneList = false;
	public bool visible = false;
	private GameObject[] Zones;
	private GameObject actif;

	void  Update() {
		// On attend que tous les objets zones soient créés
		if (!ZoneList) {
			ZoneList = GameObject.Find ("DistributeObjects").GetComponent<DistributeObjects> ().ZoneList;
			if (ZoneList) {// Une fois que la création des zones est faite...
				Zones = GameObject.FindGameObjectsWithTag ("Zone"); // On récupère la liste de toutes les zones.
			}
		}
	}

	public void OnClick()
	{
		actif = GameObject.Find ("Active_object").GetComponent<Active_object> ().active;

		if (!visible) {

			// Si les zones ne sont pas visibles on les affiche
			foreach (GameObject go in Zones) {
				go.transform.localScale = Vector3.one;
			}

			visible = true;

			// Si l'objet actif possède une zone associée, on la met en valeur
			if ((actif.name != "EmptyChild") && (actif.GetComponent<ObjectsInformations> ().geometry_mock_up != "")) {
				GameObject.Find ("Active_object").GetComponent<Active_object> ().highlight_Zone ();
			}
		}

		// Si les zones sont visibles, on les cache
		else{ 
			foreach (GameObject go in Zones) {
				go.transform.localScale = Vector3.zero;
			}

			visible = false;

			//Si l'objet actif possède une zone associée, on lui enlève la mise en valeur et on l'affiche
			if ((actif.name!="EmptyChild")&&(actif.GetComponent<ObjectsInformations> ().geometry_mock_up != "")){
				GameObject.Find ("Active_object").GetComponent<Active_object> ().highlight_Zone();
			}
		}
	}
}
