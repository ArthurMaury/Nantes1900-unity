using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bouton_fermerHierarchy : MonoBehaviour {

    public Vector2 showPos;
    public Vector2 hidePos;

    public GameObject hierarchy;
    public RectTransform hierarchy_rect;
    public float speed;

    public bool hideOnStart;

    private bool hidePanel;
    private bool showPanel;
    private bool panelIsVisible = true;

    private float startTime;
    private float lerpTime;

    public GameObject openSprite;
    public GameObject closeSprite;
    public GameObject label;


    void Start()
    {
        hierarchy_rect = hierarchy.GetComponent<RectTransform>();
        hidePanel = hideOnStart;
    }


    void Update () {

        

        if (hidePanel) {

            lerpTime = (Time.time - startTime) * speed;

            if (hierarchy_rect.anchoredPosition.x > (hidePos.x - 0.1f)) {
                hierarchy_rect.anchoredPosition = Vector2.Lerp(showPos, hidePos, lerpTime);
            }

            if (lerpTime > 1)
            {
                hidePanel = false;
                panelIsVisible = false;
            }
        }



        if (showPanel)
        {
            lerpTime = (Time.time - startTime) * speed;

            if (hierarchy_rect.anchoredPosition.x < -0.1f)
            {
                hierarchy_rect.anchoredPosition = Vector2.Lerp(hidePos, showPos, lerpTime);
            }

            if (lerpTime > 1)
            { 
                showPanel = false;
                panelIsVisible = true;
            }

            
    }


        
    }
	

	public void OnClick () {

        startTime = Time.time;

        if (panelIsVisible) {
            openSprite.transform.localScale = Vector3.zero;
            closeSprite.transform.localScale = Vector3.one;
            label.transform.localScale = Vector3.one;
            hidePanel = true;
        }

        if (!panelIsVisible) {
            openSprite.transform.localScale = Vector3.one;
            closeSprite.transform.localScale = Vector3.zero;
            label.transform.localScale = Vector3.zero;
            showPanel = true;
        }
    }
}
