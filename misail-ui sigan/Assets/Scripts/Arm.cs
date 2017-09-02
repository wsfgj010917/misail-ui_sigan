using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

    public float[] angles;
	
	void Update ()
    {
        int i = Random.Range(0, angles.Length);
        Quaternion newRot = Quaternion.Euler(angles[i], transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = newRot;
	}
}
