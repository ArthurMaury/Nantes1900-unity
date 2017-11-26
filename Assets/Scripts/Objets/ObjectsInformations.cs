using UnityEngine;
using System.Collections.Generic;

//Cette classe est présente sur chaque objet, elle acueille les infos tirées de la base
public class ObjectsInformations : MonoBehaviour {


    public string id;
    public string title;
    public string description;

	//Ajoutés pour utilisation dans le panneau détails 
	public string version;
	public string status;

    public string geometry_mock_up;
    public Vector2[] orthophoto_coo;

    public bool isSpatialized;
    public float GPS_longitude;
    public float GPS_lattitude;
    public Vector3 GPS_converted;

    public List<GameObject> objets_en_relation = new List<GameObject>();

    public GameObject label;


    void Start ()
    {

        ///////Ci dessous il s'agit de partitionner les données geometry mockup en Vecteurs2
        if (geometry_mock_up != null)
            GeometryMockUpConvert(geometry_mock_up);

    }


    //Conversion des string GeometryMockup en liste de float exploitables
    void GeometryMockUpConvert(string stringValue)
    {
        char[] separator = { '(', ')',' ',',' };
        string[] substrings = stringValue.Split(separator);

        List<Vector2> tempList = new List<Vector2>();  

        for (int i = 2; i < substrings.Length - 3; i += 2)
        {
            float X = float.Parse(substrings[i]) * 20000;
            float Y = float.Parse(substrings[i + 1]) * 4455 - 4455;

            //La taille de la photo a été divisée par 4
            X *= 0.25f;
            Y *= -0.25f;

            tempList.Add(new Vector2(X, Y));
        }

        orthophoto_coo = tempList.ToArray();
    }
}

