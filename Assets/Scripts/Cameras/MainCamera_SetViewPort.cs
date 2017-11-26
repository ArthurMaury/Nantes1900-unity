using UnityEngine;
using System.Collections;

public class MainCamera_SetViewPort : MonoBehaviour {



    private RectTransform lateralPanel_rect;
	private RectTransform Main_Canvas_rect;
    private Camera cam;


    void Start()
    {

        lateralPanel_rect = GameObject.Find("Panneau latéral").GetComponent<RectTransform>();
		Main_Canvas_rect = GameObject.Find("MainCanvas").GetComponent<RectTransform>();
        cam = gameObject.GetComponent<Camera>();


        float viewport_width = 0;

		viewport_width = 1 - (lateralPanel_rect.sizeDelta.x/Main_Canvas_rect.sizeDelta.x);

            cam.rect = new Rect(0, 0, viewport_width, 1);
        
    
    }

}
