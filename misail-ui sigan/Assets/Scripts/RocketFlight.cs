using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFlight : MonoBehaviour {

    public Transform target;
    public Transform aim;
    public float rotSpeed;
    public float rotAcc;
    public float movSpeed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(Vector3.forward * movSpeed * Time.deltaTime);

        aim.LookAt(target);

        transform.rotation = Quaternion.Lerp(transform.rotation, aim.rotation, Time.deltaTime * rotSpeed);

        rotSpeed += rotAcc;
    }
}
