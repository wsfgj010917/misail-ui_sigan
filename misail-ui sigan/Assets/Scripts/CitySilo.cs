using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySilo : MonoBehaviour {

    public Transform targets;
    public Foot foot;
    public Transform arm;
    public GameObject rocket;
    public float delay;
    float timer;

    void Start()
    {
        timer = delay;
    }

    void Update ()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = delay;
            GameObject r = Instantiate(rocket, arm.position, arm.rotation) as GameObject;
            r.GetComponent<RocketFlight>().target = targets;
        }
	}
}
