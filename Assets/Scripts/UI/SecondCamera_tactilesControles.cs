using UnityEngine;
using System.Collections;

public class SecondCamera_tactilesControles : MonoBehaviour {


    public SecondCameraControl secondCamControl;


	void Start () {
	
	}
	

	void Update () {
	
	}


    public void Zoom()
    {
        secondCamControl.zoomButton = true;
    }


    public void ZoomUp()
    {
        secondCamControl.zoomButton = false;
    }


    public void Dezoom()
    {
        secondCamControl.dezoomButton = true;
    }


    public void DezoomUp()
    {
        secondCamControl.dezoomButton = false;
    }
}
