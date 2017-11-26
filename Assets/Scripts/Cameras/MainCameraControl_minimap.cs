using UnityEngine;
using System.Collections;

public class MainCameraControl_minimap : MonoBehaviour {

    private DetectMouseOnMap detectMouseMap;

    public Vector3 nextDestination;
    private Vector3 nextDestination_check;

    public Transform lookAtPoint;
    public Vector3 lookAtPoint_pos;


    void Start () {

        detectMouseMap = GameObject.Find("Second Camera").GetComponent<DetectMouseOnMap>();

        nextDestination = new Vector3(0, 0, 0);
        nextDestination_check = new Vector3(0, 0, 0);
    }
	

	void Update () {

        ///////Déplacement par la minimap

        if (detectMouseMap.mouseOnMap && Input.GetMouseButton(1)) { 

        lookAtPoint.position = new Vector3(lookAtPoint_pos.x, lookAtPoint.position.y, lookAtPoint_pos.z);
        transform.LookAt(lookAtPoint);

            if (nextDestination != nextDestination_check)
            {
                nextDestination_check = nextDestination;
                transform.position = nextDestination + new Vector3(-15, 30, -15);
            }

        }
    }
}
