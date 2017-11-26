using UnityEngine;
using System.Collections;


//Cette class gère le déplacement de la MAinCamera à partir de la minimap
public class Orthophoto_detectClick : MonoBehaviour {


    private Camera secondCamera; 
    private MainCameraControl_minimap mcc_m;
    private Icon_camera iconCamera;



    void Start () {

        secondCamera = GameObject.Find("Second Camera").GetComponent<Camera>();
        mcc_m = GameObject.Find("Main Camera").GetComponent<MainCameraControl_minimap>();
        iconCamera = GameObject.Find("icon_camera").GetComponent<Icon_camera>();

    }


    void Update () {

        if (Input.GetMouseButtonDown(1))
            OnRightClickDown();

        if (Input.GetMouseButton(1))
            OnRightClick();
    }


    // Check for Right-Click
    void OnRightClickDown()
    {
        // Cast a ray from the mouse
        // cursors position
        Ray clickPoint = secondCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        // See if the ray collided with an object
        if (Physics.Raycast(clickPoint, out hitPoint))
        {
            // Make sure this object was the
            // one that received the right-click
            if (hitPoint.collider == this.GetComponent<Collider>())
            {
                mcc_m.nextDestination = hitPoint.point;
            }
        }
    }


    void OnRightClick()
    {
        // Cast a ray from the mouse
        // cursors position
        Ray clickPoint = secondCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        // See if the ray collided with an object
        if (Physics.Raycast(clickPoint, out hitPoint))
        {
            // Make sure this object was the
            // one that received the right-click
            if (hitPoint.collider == this.GetComponent<Collider>())
            {
                mcc_m.lookAtPoint_pos = hitPoint.point;
            }
        }
    }
}
