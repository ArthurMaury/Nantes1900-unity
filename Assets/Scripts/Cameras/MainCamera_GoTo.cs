using UnityEngine;
using System.Collections;

public class MainCamera_GoTo : MonoBehaviour {


    public Transform target_object;
    private Vector3 target_offset;
    private Vector3 start_pos;

    private Quaternion start_rot;
    private Quaternion end_rot;

    private bool goTo = false;

    public float speed;
    private float startTime;
    private float lerpTime;


    void Start () {
	
	}
	

	void Update () {

        if (goTo)
        {
            start_rot = transform.rotation;

            Vector3 dirVector = target_object.position - transform.position;
            end_rot = Quaternion.LookRotation(dirVector);

            lerpTime = (Time.time - startTime) * speed;

            transform.position = Vector3.Lerp(start_pos, target_offset, lerpTime);
            transform.rotation = Quaternion.Slerp(start_rot, end_rot, lerpTime);


            if (lerpTime > 1)
                goTo = false;
        }



	}


    public void GoTo (Transform target)
    {
        startTime = Time.time;

        target_object = target;

        start_pos = transform.position;
        start_rot = transform.rotation;

        Vector3 dirVector = target_object.position - transform.position;
		Vector3 offset = new Vector3 (dirVector.x/dirVector.magnitude*20, -20, dirVector.z/dirVector.magnitude*20);
		target_offset = target_object.position - offset;
        end_rot = Quaternion.LookRotation(dirVector);

        

        goTo = true;
    }



}
