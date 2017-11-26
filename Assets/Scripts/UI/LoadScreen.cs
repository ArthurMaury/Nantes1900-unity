using UnityEngine;
using System.Collections;

public class LoadScreen : MonoBehaviour
{


    void Start()
    {
        StartCoroutine(Wait());
    }


    void Update()
    {

    }


    IEnumerator Wait()
    {

        yield return new WaitForSeconds(3.5f);
        gameObject.SetActive(false);
    }
}