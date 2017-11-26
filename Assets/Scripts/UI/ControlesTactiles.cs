using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlesTactiles : MonoBehaviour {


    public MainCameraControl camControl;
    public Slider slide;
    public Slider slide_vert;
    public bool mouseOverMinimap;

	void Start () {
	
	}
	

	void Update () {

        if (Input.GetMouseButtonDown(1) && !mouseOverMinimap) {
            transform.position = Input.mousePosition;
        }

        camControl.slideValue = slide.value;
        camControl.slideVert_value = slide_vert.value;
	}


    public void MinimapPointerEnter()
    {
        mouseOverMinimap = true;
    }
    public void MinimapPointerExit()
    {
        mouseOverMinimap = false;
    }


    public void Zoom()
    {
        camControl.zoomButton = true;
    }

    public void ZoomUp()
    {
        camControl.zoomButton = false;
    }


    public void Dezoom()
    {
        camControl.dezoomButton = true;
    }

    public void DezoomUp()
    {
        camControl.dezoomButton = false;
    }


    public void SliderHandlerDown()
    {
        camControl.slideValue = slide.value;
    }

    public void SliderHandlerUp()
    {
        slide.value = 0;
    }


    public void SliderVertHandlerDown()
    {
        camControl.slideVert_value = slide_vert.value;
    }

    public void SliderVertHandlerUp()
    {
        slide_vert.value = 0;
    }


    public void LeftArrow()
    {
        camControl.leftArrow = true;
    }

    public void LeftArrowUp()
    {
        camControl.leftArrow = false;
    }


    public void RightArrow()
    {
        camControl.rightArrow = true;
    }

    public void RightArrowUp()
    {
        camControl.rightArrow = false;
    }


    public void UpArrow()
    {
        camControl.upArrow = true;
    }

    public void UpArrowUp()
    {
        camControl.upArrow = false;
    }


    public void DownArrow()
    {
        camControl.downArrow = true;
    }

    public void DownArrowUp()
    {
        camControl.downArrow = false;
    }
}
