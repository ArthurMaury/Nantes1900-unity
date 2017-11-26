using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondCameraControl : MonoBehaviour {


    private DetectMouseOnMap detectMouse;

    private Camera cam;
    private Curseur_croix curseurCroix;
    Vector3 clickPos;

    public float zoomSpeed = 10;

    private bool canMove;

    public float mouseX_sensitivity = 1;
    public float mouseY_sensitivity = 1;
    private float mouseX;
    private float mouseY;


    //////Contrôles tactiles
    public bool zoomButton;
    public bool dezoomButton;


    void Start () {

        detectMouse = gameObject.GetComponent<DetectMouseOnMap>();
        cam = gameObject.GetComponent<Camera>();
        curseurCroix = GameObject.Find("curseur_croix").GetComponent<Curseur_croix>();

        canMove = false;
	}
	

	void Update () {


        if (detectMouse.mouseOnMap == true)
        {

            ////////Zoom//////////

            if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > 10 || zoomButton)
            {
                cam.orthographicSize -= (zoomSpeed * Time.deltaTime);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f || dezoomButton)
            {
                cam.orthographicSize += (zoomSpeed * Time.deltaTime);
            }

        }

            mouseX = Input.GetAxis("Mouse X") * mouseX_sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseY_sensitivity;



            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    clickPos = hit.point;

            }


        //////Déplacements latéraux
        
       
        

        if (Input.GetMouseButtonDown(0))
        {
            if(detectMouse.mouseOnMap == true)
            {
                canMove = true;
            }
        }

            if (Input.GetMouseButton(0))
            {
                if(canMove == true) { 
                transform.position -= new Vector3(mouseX, 0, mouseY);
                Cursor.visible = false;
                curseurCroix.Display(new Vector3(clickPos.x, 10.1f, clickPos.z));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Cursor.visible = true;
                curseurCroix.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                if (detectMouse.mouseOnMap == true)
                    canMove = true;
                if (detectMouse.mouseOnMap == false)
                    canMove = false;
        }

    }
}
