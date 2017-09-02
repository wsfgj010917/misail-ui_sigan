using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLife : MonoBehaviour {

    public bool isDead;
    public GameObject cityAlive;
    public GameObject cityDead;

    GameManagerAndTimer managerAndTimer;

    void Start()
    {
        managerAndTimer = GameManagerAndTimer.instance;
    }

    void Update ()
    {
        //cityAlive.SetActive(!isDead);
        //cityDead.SetActive(isDead);
	}

    public void GotHit()
    {
        if (isDead) return;

        isDead = true;

        managerAndTimer.ComboBreak();
    }
}
