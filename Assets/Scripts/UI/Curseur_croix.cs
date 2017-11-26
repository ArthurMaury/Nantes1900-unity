using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Curseur_croix : MonoBehaviour {


    private SpriteRenderer spriteRend;


	void Start () {

        spriteRend = gameObject.GetComponent<SpriteRenderer>();
	
	}
	
	void Update () {


	
	}

    public void Display(Vector3 pos)
    {
        spriteRend.enabled = true;
        transform.position = pos;
    }
}
