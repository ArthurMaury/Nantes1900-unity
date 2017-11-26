using UnityEngine;
using System.Collections;

public class DetectMouseOnMap : MonoBehaviour {


    private Rect rect;

    private float mouseX;
    private float mouseY;

    public bool mouseOnMap = false;

	void Start () {

        rect = gameObject.GetComponent<Camera>().rect;

       

	}
	


	void Update () {

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        if (rect.xMin * Screen.width < mouseX && mouseX < rect.xMax * Screen.width && rect.yMin * Screen.height < mouseY && mouseY < rect.yMax * Screen.height)
        {
            mouseOnMap = true;
        }
        else
            mouseOnMap = false;

       
    }
}
