using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DrawObjectsOnMap : MonoBehaviour {

    public GameObject[] allObjects;

    public Material[] mat;

    private Transform parent;


    public GameObject emptyObject;


	void Start () {

        parent = GameObject.Find("Zones").transform;
        StartCoroutine(Wait());

         
       	
	}


    IEnumerator Wait()
    {
     
        yield return new WaitForSeconds(2f);

        allObjects = GameObject.FindGameObjectsWithTag("Objet");

         

        foreach (GameObject obj in allObjects)
        {

            ObjectsInformations infos = obj.GetComponent<ObjectsInformations>();

            if (infos.geometry_mock_up != "")
            {
                GameObject obj_zone = GameObject.Find(obj.name + "_zone");
                // Use the triangulator to get indices for creating triangles
                Triangulator tr = new Triangulator(infos.orthophoto_coo);
                int[] indices = tr.Triangulate();

                // Create the Vector3 vertices
                Vector3[] vertices = new Vector3[infos.orthophoto_coo.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = new Vector3(infos.orthophoto_coo[i].x, 0, infos.orthophoto_coo[i].y);
                }

                // Create the mesh
                Mesh msh = new Mesh();
                msh.vertices = vertices;
                msh.triangles = indices;
                msh.RecalculateNormals();
                msh.RecalculateBounds();

                // Set up game object with mesh;
                obj_zone.AddComponent(typeof(MeshRenderer));
                MeshFilter filter = obj_zone.AddComponent(typeof(MeshFilter)) as MeshFilter;
                filter.mesh = msh;

                // Set up the material
                obj_zone.GetComponent<Renderer>().material = mat[Random.Range(0, mat.Length)];

                obj_zone.AddComponent<DrawZoneLines>();

				obj_zone.AddComponent<ZoneOnClick> ();

				obj_zone.AddComponent<MeshCollider> ();

                ///////Génération des lignes

                //for (var j = 0; j < infos.orthophoto_coo.Length - 1; j++)
                //{
                //    GameObject lineObject = Instantiate(emptyObject) as GameObject;
                //    lineObject.layer = 13;
                //    lineObject.name = obj_zone.name + "_lineRenderer" + j.ToString();



                //    lineObject.transform.SetParent(obj_zone.transform);
                //    LineRenderer lineRend = lineObject.GetComponent<LineRenderer>();

                //}

            }
        }

        TransformData td = GameObject.Find("Zones_transform").transform.Clone();

        parent.position = td.position;
        parent.localPosition = td.localPosition;
        parent.localRotation = td.localRotation;
        parent.localScale = td.localScale;


        //parent.transform.localScale *= 0.144f;

        //float smooth = 100f;
        //parent.transform.position += new Vector3(172.9f, 5.5f, 165.8f);
        //Vector3 targetAngles = parent.transform.eulerAngles + 180f * Vector3.right; // what the new angles should be
        //parent.transform.eulerAngles = Vector3.Lerp(parent.transform.eulerAngles, targetAngles, smooth * Time.deltaTime);


    }

}
