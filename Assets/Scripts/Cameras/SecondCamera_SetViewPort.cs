using UnityEngine;
using System.Collections;

public class SecondCamera_SetViewPort : MonoBehaviour {


    private RectTransform Cadre_miniMap_rect;
	private RectTransform Main_Canvas_rect;
    private Camera cam;


    void Start()
    {

		Cadre_miniMap_rect = GameObject.Find("Cadre_miniMap").GetComponent<RectTransform>();
		Main_Canvas_rect = GameObject.Find("MainCanvas").GetComponent<RectTransform>();

        cam = gameObject.GetComponent<Camera>();


        float viewport_width = 0;

		float viewport_x = 1 - Cadre_miniMap_rect.sizeDelta.x / Main_Canvas_rect.sizeDelta.x;
		float viewport_height = Cadre_miniMap_rect.sizeDelta.y/Main_Canvas_rect.sizeDelta.y;
		viewport_width =  Cadre_miniMap_rect.sizeDelta.x/Main_Canvas_rect.sizeDelta.x;

        cam.rect = new Rect(viewport_x, 0, viewport_width, viewport_height);

		if (Screen.width > 5000) {
			cam.rect = new Rect (0.835f, 0, 0.165f, 0.33f);
		}
    }

}