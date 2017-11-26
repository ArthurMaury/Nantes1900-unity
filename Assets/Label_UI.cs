using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Script présent sur chaque label
public class Label_UI : MonoBehaviour {


    public RectTransform RT;

    public GameObject label_fond;
    public RectTransform label_fond_RT;

    public Text label_text;

    public GameObject label_pointe;
    public RectTransform label_pointe_RT;

    public bool isVisible = false;

    //taille du label à l'écran
    public float mult_size;

    private Camera mainCam;



    void Start () {

        RT = gameObject.GetComponent<RectTransform>();

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach(Transform tr in children)
        {
            if (tr.name == "Label_fond") { 
                label_fond = tr.gameObject;
                label_fond_RT = tr.GetComponent<RectTransform>();
            }

            if (tr.name == "Label_text")
                label_text = tr.GetComponent<Text>();

            if (tr.name == "Label_pointe")
            {
                label_pointe = tr.gameObject;
                label_pointe_RT = tr.GetComponent<RectTransform>();
            }
        }

        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        StartCoroutine(InitializeWidth());
        
	}
	

	void Update () {


        if (isVisible)
        {
            float distance = Vector3.Distance(mainCam.transform.position, transform.position);

            float size = (mainCam.transform.position - transform.position).magnitude;
            size = size * mult_size;
            label_pointe_RT.localScale = new Vector3(size, size, size);
        }

        

	
	}


    IEnumerator InitializeWidth()
    {
        yield return new WaitForSeconds(1);
        Vector2 newSize = new Vector2(label_text.text.Length * 1.45f, label_fond_RT.rect.height);
        label_fond_RT.sizeDelta = newSize;
        label_pointe_RT.localScale = Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Label_zone") { 
            label_pointe_RT.localScale = Vector3.one;
            isVisible = true;

            
            Label_zone zone_script = other.transform.parent.GetComponent<Label_zone>();
            zone_script.labels.Add(gameObject);
            
        }
    }
}
