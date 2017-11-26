using UnityEngine;
using System.Collections;


//Fait en sorte qu'un objet soit toujours orienté face à la caméra
public class CameraFacingBillboard : MonoBehaviour
{
    //caméra vers laquelle s'orienter
    public Camera m_Camera;


    void Start()
    {

        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }


    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}