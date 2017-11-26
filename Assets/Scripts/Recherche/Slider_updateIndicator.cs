using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slider_updateIndicator : MonoBehaviour {


    public Text text_to_update;
    public Recherche researchField;

	public void UpdateIndicator ()
    {
        text_to_update.text = gameObject.GetComponent<Slider>().value.ToString();
        researchField.profondeur_de_relations = Mathf.RoundToInt (gameObject.GetComponent<Slider>().value);
    }
}
