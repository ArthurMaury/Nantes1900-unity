using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Cette class gère la fonction de recherche,
//elle se met en marche lorsqu'il y a clic sur le champ de texte
public class Recherche : MonoBehaviour {
 
    //le bool passe à truez lors d'un clic sur le champ de texte
    private bool researchIsActive;

    public UI_ToolBox UI_ToolBox;
    //private CameraControl camControl;

    private ReadObjects readObjects;

    //liste des titres des objets
    public List<string> objets_titles = new List<string>();
    //liste de suggestions établie à partir des caractères entrés dans le champ de texte
    public List<string> objets_suggestions = new List<string>();
    //liste des objets correspondant aux suggestions
    public List<GameObject> suggestions_currentsGameObjects = new List<GameObject>();

    private Slider slider_profondeursRelations;

    public GameObject[] objets_all;

    char[] delimeterChars = { ' ', ',', '.', ':' };

    public Text text_Placeholder;
    //le texte actuellement présent dans le champ de recherche
    public Text current_Text;

    public GameObject suggestion_panel;
    private float suggesttion_Ypos;
    private int suggestion_highlighted = 1;

    public Color normal_color;
    public Color highlight_color;

    public GameObject lineRenderer;

    ///////////Paramètres de recherche///////////
    public string research_name;
    //le "check" est là pour repérer un changement de valeur d'entrée du champ de texte
    private string research_name_checkChange;
    public int profondeur_de_relations = 0;

    public GameObject objectSearched = null;


    void Start () {

        researchIsActive = false;

        UI_ToolBox = GameObject.Find("UI_toolbox").GetComponent<UI_ToolBox>();
        //camControl = GameObject.Find("Main Camera").GetComponent<CameraControl>();

        objets_all = GameObject.FindGameObjectsWithTag("Objet");

        foreach(GameObject go in objets_all)
        {
            objets_titles.Add(go.GetComponent<ObjectsInformations>().title);
        }


        slider_profondeursRelations = GameObject.Find("Slider profondeurs relations").GetComponent<Slider>();

        text_Placeholder = GameObject.Find("Champ_texte").transform.GetChild(0).GetComponent<Text>();
        current_Text = GameObject.Find("Champ_texte").transform.GetChild(1).GetComponent<Text>();

        research_name_checkChange = research_name;


    }
	

    void Update()
    {
        ///On vérifie si la valeur de objectSearched change, auquel cas on affiche directement le panneau///
        if(research_name != research_name_checkChange)
        {
            if (research_name != null) {
                UI_ToolBox.ShowUI(transform.parent.gameObject);
                gameObject.GetComponent<InputField>().text = research_name;               
            }

            research_name_checkChange = research_name;
        }


        if (researchIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CancelResearch();

            if (Input.GetKeyDown(KeyCode.DownArrow) && suggestion_highlighted < suggestions_currentsGameObjects.Count - 1)
                suggestion_highlighted += 1;
            if (Input.GetKeyDown(KeyCode.UpArrow) && suggestion_highlighted > 0)
                suggestion_highlighted -= 1;

            if (suggestions_currentsGameObjects.Count != 0)
            {                
                foreach (GameObject go in suggestions_currentsGameObjects)
                {
                    if (go != null && go.name == "suggestion" + suggestion_highlighted as string)
                    {
                        go.GetComponent<Image>().color = highlight_color;                      
                        if (Input.GetKeyDown(KeyCode.Return)) { 
                            research_name = go.GetComponentInChildren<Text>().text;
                            gameObject.GetComponent<InputField>().text = go.GetComponentInChildren<Text>().text;
                            for (var i = 0; i < suggestions_currentsGameObjects.Count; i++)
                                Destroy(suggestions_currentsGameObjects[i]);
                        }
                    }
                    else if(go != null)
                        go.GetComponent<Image>().color = normal_color;
                }
            }
        }
    }


    ///////////Se déclenche par un clic sur la barre de recherche///////////

    public void OnClick()
    {
        researchIsActive = true;
    }


    ///////////Chercher les mot-clés dans la liste des titres des objets///////////

    public void LaunchResearch (string research)
    {
        objets_suggestions.Clear();       

        string[] words = current_Text.text.Split(delimeterChars);

        foreach (string word in words)
        {
            for (var i = 0; i < objets_titles.Count; i++)
            {
                if (objets_titles[i] != null && objets_titles[i].IndexOf(word, System.StringComparison.OrdinalIgnoreCase) != -1)            
                    objets_suggestions.Add(objets_titles[i]);
            }
        }
        DisplaySuggestion();
    }


    ///////////Afficher la liste de suggestions sous la barre de recherche///////////

    public void DisplaySuggestion()
    {
        suggestion_highlighted = 0;
        suggesttion_Ypos = -29.5f;


        for (var i = 0; i < suggestions_currentsGameObjects.Count; i++) 
            Destroy(suggestions_currentsGameObjects[i]);

        
        suggestions_currentsGameObjects.Clear();

            for (var i = 0; i < objets_suggestions.Count; i++)
        {

            GameObject obj = Instantiate(suggestion_panel);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.SetAsLastSibling();
            obj.name = "suggestion" + i as string;
            suggestions_currentsGameObjects.Add(obj);


            suggesttion_Ypos -= 29.5f;
            Vector3 pos = new Vector3(0, suggesttion_Ypos, 0);
            obj.transform.localPosition = pos;

            obj.GetComponentInChildren<Text>().text = objets_suggestions[i];
        }

    }


    ///////////Valider la recherche///////////

    public void ValideResearch()
    {
        print("validé");
        ObjectsInformations objectSearched_infos = null;
        ObjetOnClick objetSearched_onClick = null;
        List<List<GameObject>> objets_relations_all = new List<List<GameObject>>();

        researchIsActive = false;

        print("objet de la recherche : '" + research_name +"'"); 

        foreach(GameObject go in objets_all)
        {
            ///Trouver l'objet par correspondance de titre...///
            ObjectsInformations infos = go.GetComponent<ObjectsInformations>();
            if (infos.title == research_name)
            {
                objectSearched = go;
                objectSearched_infos = objectSearched.GetComponent<ObjectsInformations>();
                objetSearched_onClick = objectSearched.GetComponent<ObjetOnClick>();
            }
        }

        ///Si l'objet n'a pas de relations, afficher direct les infos///
        if(objectSearched_infos.objets_en_relation.Count == 0 || profondeur_de_relations == 0)
        {
            objetSearched_onClick.DisplayLabel();
            SetupCamera(objectSearched);
        }

        ///S'il en a, établir une liste de liste de tout les objets en relation///
        if (objectSearched_infos.objets_en_relation.Count != 0 && profondeur_de_relations != 0)
        {

            ///La liste 0 est uniquement constituée de l'objet recherché///
            objets_relations_all.Add(new List<GameObject>());
            objets_relations_all[0].Add(objectSearched);


            for (var i = 1; i < profondeur_de_relations; i++)
            {
                objets_relations_all.Add(new List<GameObject>());

                for (var j = 0; j < objets_relations_all[i - 1].Count; j++)
                {
                    ObjectsInformations infos = objets_relations_all[i - 1][j].GetComponent<ObjectsInformations>();

                    if (infos.objets_en_relation != null)
                    {
                        for (var k = 0; k < infos.objets_en_relation.Count; k++)
                        {
                            objets_relations_all[i].Add(infos.objets_en_relation[k]);
                        }
                    }
                }

            }


            ///Afficher enfin toute les informations///

            int compteTotal = 0;
            float r = 255f;

            for (var i = 0; i < objets_relations_all.Count; i++)
            {
                r -= 255/profondeur_de_relations;
               
                for (var j = 0; j < objets_relations_all[i].Count; j++)
                {
                    ///Affichage des titres///
                    objets_relations_all[i][j].GetComponent<ObjetOnClick>().DisplayLabel();

                    ///Incrémentation de la couleur///
                    Renderer rend = objets_relations_all[i][j].GetComponent<Renderer>();
                    rend.material.color = new Color(r/255, 0, 0, 1);

                    compteTotal++;                 
                }
            }
            print("Au total : " + compteTotal);

            ///L'affichage des lignes de relation se fait dans une loop à part///

            float lineWidht = 0.8f;
            Color lineColor = new Color();
            float rCol = 1;

            for (var i = 0; i < objets_relations_all.Count - 1; i++)
            {

                lineWidht -= (lineWidht / objets_relations_all.Count);

                rCol -= 0.16f;

                lineColor.a = rCol;
                lineColor.r = rCol;
                lineColor.g = 0;
                lineColor.b = 0;


                ///Deux distributions pour deux caméras///
                for (var j = 0; j < objets_relations_all[i].Count; j++)
                {
                    ObjectsInformations infos = objets_relations_all[i][j].GetComponent<ObjectsInformations>();
                    if (infos.objets_en_relation != null)
                    {
                        for(var k = 0; k < infos.objets_en_relation.Count; k++)
                        {
                            GameObject lineObject = Instantiate(lineRenderer);
                            lineObject.transform.SetParent(GameObject.Find("Relations").transform);
                            lineObject.layer = 12;
                            WorldSpaceLines lineInit = lineObject.GetComponent<WorldSpaceLines>();

                            lineInit.camDisplay = Cam.mainCam;
                            lineInit.startObject = objets_relations_all[i][j];
                            lineInit.endObject = infos.objets_en_relation[k];
                        }
                    }
                }
             
            }
            

            SetupCamera(objectSearched);
            objectSearched.transform.position += new Vector3(0, 20, 0);
            
        }



    }




    ///////////Annuler la recherche, il ne s'agit là que du panneau///////////

    public void CancelResearch()
    {
        

        for (var i = 0; i < suggestions_currentsGameObjects.Count; i++)
            Destroy(suggestions_currentsGameObjects[i]);

        suggestions_currentsGameObjects.Clear();
        objets_suggestions.Clear();

        researchIsActive = false;

        gameObject.GetComponent<InputField>().text = "";
    }



    ///////////Annuler la recherche, il s'agit de toutes les informations situées dans la vue 3D///////////

    public void DeleteResearch()
    {


    }



    ///////////Setup de la caméra///////////

    public void SetupCamera(GameObject obj)
    {
        //camControl.ChangeRotationPoint(new Vector3(obj.transform.position.x,
        //                                        obj.transform.position.y + 20,
        //                                        obj.transform.position.z));

        //camControl.MoveToObject(new Vector3(obj.transform.position.x,
        //                                    obj.transform.position.y +20,
        //                                    obj.transform.position.z));
    }



    ///Trouver la position moyenne d'une liste d'objets
   
    public void FindAveragePositions()
    {



    }

}
