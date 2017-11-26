using UnityEngine;
using System.Collections;


//Bouton permettant de masquer tout les labels
public class HideAllLabels : MonoBehaviour {


	void Start () {
	
	}
	
    //On trouve les labels grâçe à un tag et réduit la "localScale" de chacun à 0
	public void HideLabels () {

        GameObject[] labels = GameObject.FindGameObjectsWithTag("Label");

        foreach(GameObject go in labels)
        {

            Label_UI labelScript = go.GetComponent<Label_UI>();
            labelScript.label_pointe_RT.localScale = Vector3.zero;
            labelScript.isVisible = false;
        }

	}
}
