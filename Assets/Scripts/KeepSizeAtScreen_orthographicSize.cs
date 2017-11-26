using UnityEngine;
using System.Collections;

public class KeepSizeAtScreen_orthographicSize : MonoBehaviour {

    public float mult;

    public Camera SecondCam;
    private float orthoSizeMax;


    void Start()
    {

        SecondCam = GameObject.Find("Second Camera").GetComponent<Camera>();
        orthoSizeMax = SecondCam.orthographicSize;

    }


    void Update()
    {


        float size = SecondCam.orthographicSize;
        size = size * mult;
        transform.localScale = new Vector3(size, -size, size);
    }


}

