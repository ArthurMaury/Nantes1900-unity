using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bouton_goTo : MonoBehaviour {


    private MainCamera_GoTo mainCam_goTo;
    private GameObject[] allObjects;
    private GameObject activeObject;

    private Text title_field;
	private Label_UI label_script;


    void Start () {

        mainCam_goTo = GameObject.Find("Main Camera").GetComponent<MainCamera_GoTo>();

        title_field = GameObject.Find("text_title").GetComponent<Text>();

        allObjects = GameObject.FindGameObjectsWithTag("Objet");
    }


    void Update () {
	
	}


    public void OnClick()
    {
        if(title_field.text != "")
        {
            foreach (GameObject go in allObjects)
            {
                ObjectsInformations infos = go.GetComponent<ObjectsInformations>();
                if(infos.title == title_field.text)
                {
                    activeObject = go;
					label_script = infos.label.GetComponent<Label_UI>();
					label_script.label_pointe_RT.localScale = Vector3.one;
					label_script.isVisible = true;
                    break;
                }
            }
        }


        mainCam_goTo.GoTo(activeObject.transform);

    }
}
