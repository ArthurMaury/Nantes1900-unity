using UnityEngine;
using System.Collections;


//Contient les fonctions servant à afficher ou masquer
//les éléments d'interface
public class UI_ToolBox : MonoBehaviour {


    public void HideUI(GameObject go)
    {
        go.transform.localScale = Vector3.zero;
    }


    public void ShowUI(GameObject go)
    {
        go.transform.localScale = Vector3.one;
    }
}
