using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamGarbage : MonoBehaviour {

    public float deadTime = 5f;
	
	void Update ()
    {
        deadTime -= Time.deltaTime;

        if (deadTime <= 0) { Destroy(gameObject); }
	}
}
