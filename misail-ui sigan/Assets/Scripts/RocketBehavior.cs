using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour {

    public float hp;
    float hpLeft;
    public int hour;
    public int minute;
    public float letMeStart;

    GameManagerAndTimer managertimer;

    void Start()
    {
        managertimer = GameManagerAndTimer.instance;

        hpLeft = hp;
    }
    
	void Update ()
    {
        if (hour == managertimer.globalHour && minute == managertimer.globalMinute)
        {
            hpLeft -= Time.deltaTime;

            if (hpLeft <= 0)
            {
                Explode(false);
            }
        }
        else
        {
            hpLeft = hp;
        }

        letMeStart -= Time.deltaTime;
	}

    void Explode(bool inAir)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (letMeStart < 0)
        {
            managertimer.explosionAnim.SetTrigger("Explode");
            Explode(false);
        }
    }
}
