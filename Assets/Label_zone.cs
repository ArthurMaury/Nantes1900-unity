using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Label_zone : MonoBehaviour {

    public float opacityMin;

    public List<GameObject> labels = new List<GameObject>();

    


	void Start () {
	
	}


    void Update () {
	
	}


    public void SetDisplay()
    {
        ///////On trouve l'objet le plus éloigné du centre de la zone

        Transform farthestObject = FindFarthestLabel();

        ///////On incrémente l'opacité et la taille
        for (var i = 0; i < labels.Count; i++)
        {
            GameObject label = labels[i];
            Label_UI label_script = label.GetComponent<Label_UI>();
            float distanceMax = Vector3.Distance(farthestObject.position, transform.position);
            Vector3 compensatedPos = new Vector3(transform.position.x, 9, transform.position.z);
            float distance = Vector3.Distance(label.transform.position, compensatedPos);
            float distancePercentage = 1 - (distance / distanceMax);


            print(distance +" / " + distanceMax + " = " + distancePercentage);
           

            Color currentCol = label_script.label_fond.GetComponent<Image>().color;
            Color newColor = new Color(currentCol.r, currentCol.g, currentCol.b, Mathf.Lerp(opacityMin, 1, distancePercentage));
            label_script.label_fond.GetComponent<Image>().color = newColor;


        }

    }


    void OnDestroy()
    {
        for (var i = 0; i < labels.Count; i++)
        {
            GameObject label = labels[i];
            Label_UI label_script = label.GetComponent<Label_UI>();
            label_script.isVisible = false;
            label_script.label_pointe_RT.localScale = Vector3.zero;

        }
    }


    public Transform FindFarthestLabel()
    {

        Transform farthest = null;

        for (var i = 0; i < labels.Count; i++)
        {
            GameObject label = labels[i];
            float distanceMax = 0.00f;

            Vector3 compensatedPos = new Vector3(transform.position.x, 9, transform.position.z);
            float distance = Vector3.Distance(label.transform.position, compensatedPos);

            if (distance > distanceMax)
            {
                distanceMax = distance;
                farthest = label.transform;
            }
        }

        return farthest;

    }
}
