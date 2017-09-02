using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySilo : MonoBehaviour {

    public Transform[] targets;
    public Foot foot;
    public Transform arm;
    public GameObject rocket;
    int i;
    
    void Update ()
    {
        i = Random.Range(0, targets.Length);
        foot.target = targets[i];        
	}

    public void LaunchRocket(int setHRS, int setMNS)
    {
        GameObject r = Instantiate(rocket, arm.position, arm.rotation) as GameObject;
        r.GetComponent<RocketFlight>().target = targets[i];
        r.GetComponent<RocketBehavior>().hour = setHRS;
        r.GetComponent<RocketBehavior>().minute = setMNS;
    }
}
