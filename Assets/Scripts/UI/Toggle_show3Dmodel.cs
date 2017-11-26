using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toggle_show3Dmodel : MonoBehaviour {

    public GameObject maquette;
    public GameObject ground;
    public GameObject hemisphere;

    private Toggle toggle;


	void Start () {

        toggle = gameObject.GetComponent<Toggle>();
	
	}
	

	public void OnValueChanged () {

        if (toggle.isOn)
        {
            maquette.SetActive(true);
            ground.SetActive(true);
            hemisphere.SetActive(true);
        }

        if (!toggle.isOn)
        {
            maquette.SetActive(false);
            ground.SetActive(false);
            hemisphere.SetActive(false);
        }

    }
}
