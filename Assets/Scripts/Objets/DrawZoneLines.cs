using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DrawZoneLines : MonoBehaviour
{


    public Transform[] lineRenderers;
    private List<Transform> lineRenderers_list = new List<Transform>();

    public LineRenderer[] lineRenderers_comp;
    private List<LineRenderer> lineRenderers_comp_list = new List<LineRenderer>();

    private Mesh mesh;
    public Vector3[] vertices;


    void Start()
    {
        StartCoroutine(Wait());

    }


    void Update()
    {

        
    }


    IEnumerator Wait()
    {

        yield return new WaitForSeconds(2f);

        lineRenderers = gameObject.GetComponentsInChildren<Transform>();
        lineRenderers_comp = gameObject.GetComponentsInChildren<LineRenderer>();

        lineRenderers_list = lineRenderers.ToList();
        lineRenderers_comp_list = lineRenderers_comp.ToList();

        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;


        for(var i = 1; i < lineRenderers_list.Count; i++)
        {
            lineRenderers_list[i].localPosition = vertices[i];
        }

        for (var i = 0; i < lineRenderers_comp_list.Count - 1; i++)
        {
            lineRenderers_comp_list[i].SetPosition(0, lineRenderers_list[i+1].localPosition);
            lineRenderers_comp_list[i].SetPosition(1, lineRenderers_list[i + 2].localPosition);
        }
    }

}