using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnStart : MonoBehaviour {

    public UnityEvent onStart;

    void Start()
    {
        onStart.Invoke();
    }
}
