using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Toggle_controlesTactiles : MonoBehaviour {

    
    public Transform controleTactiles;

    private Toggle toggle;


    void Start()
    {

        toggle = gameObject.GetComponent<Toggle>();

    }


    public void OnValueChanged()
    {

        if (toggle.isOn)
        {           
            controleTactiles.localScale = Vector3.one;
        }

        if (!toggle.isOn)
        {
            controleTactiles.localScale = Vector3.zero;
        }

    }
}