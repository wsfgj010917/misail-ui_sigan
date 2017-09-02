using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour {

    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void Update ()
    {
        transform.LookAt(target);
	}
}
