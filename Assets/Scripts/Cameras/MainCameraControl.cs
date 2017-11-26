using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour {

    public bool activeTravelling = false;

    public float marges = 10;
    public float translationSpeed = 10;
    public float zoomSpeed = 10;
    public float rotationSpeed = 10;
    public float mouseX_sensitivity = 1;
    public float mouseY_sensitivity = 1;
    public float Yrotation_clampMin = 5;
    public float Yrotation_clampMax = 90;
    public float rotationPoint_distance = 10;

    public float translationSpeedRelativeToHeight = 1;
    public float rotationSpeedRelativeToHeight = 1;
    public float zoomSpeedRelativeToHeight = 1;

    public bool inRotation = false;
    private bool useDefaultRotationPoint = false;
    private Transform rotationPoint_default;
    private Transform rotationPointBack_transform;
    private Vector3 rotationPoint_set;

    private Transform camera_system;
    private Transform camera_directions;
    private Transform camera_forward;
    private Transform camera_back;
    private Transform camera_right;
    private Transform camera_left;

    ///////Controles tactiles
    public bool leftArrow;
    public bool rightArrow;
    public bool upArrow;
    public bool downArrow;
    public bool zoomButton;
    public bool dezoomButton;
    public float slideValue;
    public float slideVert_value;


    [HideInInspector]
    public float mouseX;
    [HideInInspector]
    public float mouseY;
    private Vector3 mousePosReference;

    private DetectMouseOnMap detectMouseMap;


    private bool canRotate;
    private bool canMove = true;

    public GameObject focusPoint;



    void Start () {

        rotationPoint_default = GameObject.Find("Point de rotation_forward").transform;
        rotationPointBack_transform = GameObject.Find("Point de rotation_back").transform;

        camera_system = GameObject.Find("Main Camera System").transform;
        camera_directions = GameObject.Find("Camera directions").transform;
        camera_forward = GameObject.Find("camera_forward").transform;
        camera_back = GameObject.Find("camera_back").transform;
        camera_left = GameObject.Find("camera_left").transform;
        camera_right = GameObject.Find("camera_right").transform;

        detectMouseMap = GameObject.Find("Second Camera").GetComponent<DetectMouseOnMap>();

    }


    void Update () {

        camera_directions.position = gameObject.transform.position;

        Vector3 euler = transform.rotation.eulerAngles;
        Quaternion rot = Quaternion.Euler(0, euler.y, 0);
        camera_directions.rotation = rot;




        ///////Adaptation de la vitesse de rotation et de translation à la hauteur de la caméra///////

        translationSpeed = transform.position.y * translationSpeedRelativeToHeight;
        rotationSpeed = transform.position.y * rotationSpeedRelativeToHeight;
        zoomSpeed = transform.position.y * zoomSpeedRelativeToHeight;


        /////////Déplacements latéraux////////

        if (activeTravelling)
        {

            if (Input.mousePosition.x < marges && canMove || leftArrow)
                transform.position = Vector3.MoveTowards(transform.position, camera_left.position, translationSpeed * Time.deltaTime);

            if (Input.mousePosition.x > Screen.width - marges && canMove || rightArrow)
                transform.position = Vector3.MoveTowards(transform.position, camera_right.position, translationSpeed * Time.deltaTime);

            if (Input.mousePosition.y < marges && canMove || downArrow)
                transform.position = Vector3.MoveTowards(transform.position, camera_back.position, translationSpeed * Time.deltaTime);

            if (Input.mousePosition.y > Screen.height - marges && canMove || upArrow)
                transform.position = Vector3.MoveTowards(transform.position, camera_forward.position, translationSpeed * Time.deltaTime);
        }

       ////////Zoom//////////

       if(detectMouseMap.mouseOnMap == false) { 
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || zoomButton)
                transform.position = Vector3.MoveTowards(transform.position, rotationPoint_default.position, zoomSpeed * Time.deltaTime);
           

            if (Input.GetAxis("Mouse ScrollWheel") < 0f || dezoomButton)
                transform.position = Vector3.MoveTowards(transform.position, rotationPointBack_transform.position, zoomSpeed * Time.deltaTime);
          
        }

        ///////Rotations//////

        Vector3 direction = (rotationPoint_default.position - transform.position).normalized;
        Vector3 rotationPoint = new Vector3(0, 0, 0);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 5000))
        {
            focusPoint.transform.position = hit.point;

            if (hit.distance > 30)
                rotationPoint = rotationPoint_default.position;

            if (hit.distance <= 30)
                rotationPoint = hit.point;
        }



        mouseX = Input.GetAxis("Mouse X") * mouseX_sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseY_sensitivity;

        if (canRotate)
        {
            inRotation = true;
            if (!useDefaultRotationPoint)
            {
                transform.RotateAround(rotationPoint, rotationPoint_default.up, mouseX * Time.deltaTime);
                transform.RotateAround(rotationPoint, -rotationPoint_default.right, mouseY * Time.deltaTime);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
                canMove = false;
            }

            if (useDefaultRotationPoint)
            { 
                transform.RotateAround(rotationPoint_set, Vector3.up, mouseX * Time.deltaTime);
                transform.RotateAround(rotationPoint_set, -Vector3.right, mouseY * Time.deltaTime);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
                canMove = false;
            }
        }
        else
        {
            canMove = true;
            inRotation = false;
        }


        if (Input.GetMouseButtonDown(2))
            canRotate = true;

        if (Input.GetMouseButtonUp(2))
            canRotate = false;


        ///////Rotations tactiles
        if(slideValue > 0 || slideValue < 0) { 
            transform.RotateAround(rotationPoint, rotationPoint_default.up, slideValue * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
        if (slideVert_value > 0 || slideVert_value < 0)
        {
            transform.RotateAround(rotationPoint, -rotationPoint_default.right, -slideVert_value * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
    }

}
