using UnityEngine;
using System.Collections;


public enum Cam { mainCam, secondCam };

public class WorldSpaceLines : MonoBehaviour {

    private LineRenderer lineRend;

    public Cam camDisplay;

    private Camera mainCam;
    private Camera secondCam;

    public GameObject startObject;
    public GameObject endObject;

    private Vector3 startPos;
    private Vector3 endPos;

    public float mainCamWidth;
    public float secondCamWidth;





    void Awake()
    {
        gameObject.name = "LineRenderer";
        gameObject.tag = "LineRenderer";
    }



    void Start () {

        lineRend = gameObject.GetComponent<LineRenderer>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        secondCam = GameObject.Find("Second Camera").GetComponent<Camera>();

        startPos = startObject.transform.position;
        endPos = endObject.transform.position;

        lineRend.SetPosition(0, startPos);
        lineRend.SetPosition(1, endPos);

        if (camDisplay == Cam.mainCam)
            gameObject.layer = 12;

        if (camDisplay == Cam.secondCam)
            gameObject.layer = 12;

    }
	

	void Update () {

        if (camDisplay == Cam.mainCam)
        {
            lineRend.SetWidth(mainCamWidth, mainCamWidth);
        }

        if (camDisplay == Cam.secondCam)
        {
            lineRend.SetWidth(secondCamWidth, secondCamWidth);
        }

    }



}
