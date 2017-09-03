using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketBehavior : MonoBehaviour {

    public float hp;
    float hpLeft;
    public int hour;
    public int minute;
    public float letMeStart;
    public Text clock;

    public GameObject nuke;
    public GameObject inAirExplosion;

    GameManagerAndTimer managertimer;

    void Start()
    {
        managertimer = GameManagerAndTimer.instance;

        hpLeft = hp;

        clock.text = hour + ":" + minute;
    }
    
	void Update ()
    {
        if (hour == managertimer.globalHour && minute == managertimer.globalMinute)
        {
            hpLeft -= Time.deltaTime;

            if (hpLeft <= 0)
            {
                Explode(true);
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
        if (inAir)
        {
            managertimer.Success();
            Instantiate(inAirExplosion, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (letMeStart < 0)
        {
            managertimer.explosionAnim.SetTrigger("Explode");
            other.gameObject.GetComponent<CityLife>().GotHit();

            GameObject n = Instantiate(nuke, other.transform.position, nuke.transform.rotation) as GameObject;
            n.GetComponent<Nuke>().Detonate();
            
            Explode(false);
        }
    }
}
