using UnityEngine;
using System.Collections;

public class Icon_camera : MonoBehaviour {

    private Transform mainCam;

	void Start () {

        mainCam = GameObject.Find("Main Camera").transform;

	}
	

	void Update () {

        transform.position = new Vector3(mainCam.position.x, 0.5f, mainCam.position.z);

    }


}
