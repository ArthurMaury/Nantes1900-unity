using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using UnityEngine.Networking;

public class ReadObjects : MonoBehaviour
{

    //Cette classe est présente sur tout les objets
    private ObjectsInformations objectInformations;

    //Un gameObject vide pour accueillir les "ObjetcsInformations"
    public GameObject prefab;
    //La position à laquelle on instancie les objets
    private Vector3 pos = Vector3.zero;


    public List<GameObject> allObjects = new List<GameObject>();
    public List<GameObject> spatializedObjects = new List<GameObject>();
    public List<GameObject> noSpatializedObjects = new List<GameObject>();
    //Tentative de regrouper les objets qui partagent les mêmes coordonnées dans une liste de liste
    public List<List<GameObject>> sharedCoordinates = new List<List<GameObject>>();

    private string[] parent = { "Objects_mainCamera", "Objects_secondCamera" };


    void Awake()
    {


        ///////////Créer les objets et assigner les informations////////////

        var hostName = "http://localhost";
        var port = ":3000";

        #region getting objects

        try
        {
            var uriObjects = hostName + port + "/Objects";
            var objects = GetObjectList<ObjectDB>(uriObjects);


            foreach (var myObj in objects)
            {
                GameObject obj;

                obj = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
                obj.name = "object" + myObj.Id as string;
                obj.transform.SetParent(GameObject.Find("Objects").transform);
                obj.GetComponent<ObjectsInformations>().id = myObj.Id.ToString();
                obj.GetComponent<ObjectsInformations>().title = myObj.Title;
                obj.GetComponent<ObjectsInformations>().description = myObj.Abstract;
                obj.GetComponent<ObjectsInformations>().version = myObj.VersionNb.ToString();
                obj.GetComponent<ObjectsInformations>().status = myObj.Status;
                obj.GetComponent<ObjectsInformations>().geometry_mock_up = myObj.Geometry;


                allObjects.Add(obj);
            }
        }
        catch (Exception e)
        {
            throw e;
        }




        #endregion

        #region getting Relations

        try
        {

            var uriRelations = hostName + port + "/Relations";
            var relations = GetObjectList<Relation>(uriRelations);

            foreach (var relation in relations)
            {
                GameObject obj = GameObject.Find("object" + relation.A as string);
                GameObject objRelated = GameObject.Find("object" + relation.B as string);
                ObjectsInformations comp = obj.GetComponent<ObjectsInformations>();
                comp.objets_en_relation.Add(objRelated);
            }

        }
        catch (Exception e)
        {
            throw e;
        }

        #endregion

        #region getting spatial data

        try
        {
            var uriSpatiale = hostName + port + "/Spatial";
            var spatials = GetObjectList<Spatial>(uriSpatiale);

            foreach (var spatial in spatials)
            {
                GameObject obj = GameObject.Find("object" + spatial.Id);
                ObjectsInformations comp = obj.GetComponent<ObjectsInformations>();

                if (spatial.Longitude.HasValue && spatial.Latitude.HasValue)
                {
                    comp.isSpatialized = true;
                    comp.GPS_longitude = spatial.Longitude.Value;
                    comp.GPS_lattitude = spatial.Latitude.Value;
                    comp.GPS_converted = GPSEncoder.GPSToUCS(new Vector2(spatial.Latitude.Value, spatial.Longitude.Value));
                    spatializedObjects.Add(obj);

                }

                else
                {
                    comp.isSpatialized = false;
                    noSpatializedObjects.Add(obj);
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }



        #region group

        /*try
        {
            foreach (var spatial_i in spatials)
            {
                GameObject obj_i = GameObject.Find("object" + spatial_i.Id);
                ObjectsInformations comp_i = obj_i.GetComponent<ObjectsInformations>();

                foreach (var spatial_j in spatials)
                {
                    GameObject obj_j = GameObject.Find("object" + spatial_j.Id);

                    if (spatial_i.Id != spatial_j.Id &&
                        comp_i.GPS_lattitude > 0 &&
                        spatial_i.Latitude == spatial_j.Latitude &&
                        spatial_i.Longitude == spatial_j.Longitude)
                    {
                        sharedCoordinates.Add(new List<GameObject>() { obj_i, obj_j });
                    }
                }
            }

            Debug.Log(sharedCoordinates.Count);
        }
        catch (Exception e)
        {

            throw e;
        }*/


        #endregion


        #endregion


    }

    string GetRequest(string uri)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            www.Send();

            while (!www.isDone && !www.isNetworkError)
            {

            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                throw new Exception("Couldn't connect to the Database");
            }
            else
            {
                return www.downloadHandler.text;
            }
        }
    }

    List<T> GetObjectList<T>(string uri)
    {
        try
        {
            UnityWebRequest www = UnityWebRequest.Get(uri);

            string objectsJson = GetRequest(uri);
            var objects = JsonConvert.DeserializeObject<List<T>>(objectsJson);

            return objects;
        }
        catch (Exception e)
        {
            throw e;
        }


    }

}

[Serializable]
public class Relation
{
    public int A;
    public int B;
}

[Serializable]
public class ObjectDB
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }
    public int VersionNb { get; set; }
    public string Status { get; set; }
    //public IList<string> Keywords { get; set; }
    public string Geometry { get; set; }
}

[Serializable]
public class Spatial
{
    public int Id { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
}