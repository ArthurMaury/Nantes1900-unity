using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeepSizeAtScreen : MonoBehaviour {


    public float mult;

    public GameObject MainCam;
    private float distanceMax;


	void Start () {
	
        MainCam = GameObject.Find("Main Camera");
        distanceMax = Vector3.Distance(MainCam.transform.position, transform.position);

    }
	


	void Update () {

    float distance = Vector3.Distance(MainCam.transform.position, transform.position);

    float size = (Camera.main.transform.position - transform.position).magnitude;
    size = size * mult;
    transform.localScale = new Vector3(size, -size, size);
    }


}
