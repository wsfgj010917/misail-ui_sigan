using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAndTimer : MonoBehaviour {

    public int globalHour;
    public int globalMinute;
    public Text timer;

    int dirHRS;
    int spdHRS;
    int dirMNS;
    int spdMNS;

    float hrsClock;
    float hrsClockFast;
    float mnsClock;
    float mnsClockFast;

    public CitySilo[] silos;

    public int[] latestHRS;
    public int[] latestMNS;

    public float minDelay;
    public float minDelayABS;
    public float maxDelay;
    public float maxDelayABS;
    float delay;
    public int levelUpAfter;
    public int levelUpMultiplier;
    int levelUpCounter;
    public float delayDecrease;

    public Animator explosionAnim;

    public static GameManagerAndTimer instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        delay = Random.Range(minDelay, maxDelay);
        levelUpCounter = levelUpAfter;

        for (int i = 0; i < latestHRS.Length; i++)
        {
            latestHRS[i] = Random.Range(0, 12);
        }
        for (int i = 0; i < latestMNS.Length; i++)
        {
            latestMNS[i] = Random.Range(0, 12) * 5;
        }

        dirHRS = 1;
        dirMNS = 1;

        hrsClock = 1f;
        hrsClockFast = 0.2f;
        mnsClock = 0.5f;
        mnsClockFast = 0.1f;
    }

    
    void Update ()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            delay = Random.Range(minDelay, maxDelay);

            ArmAndShoot();

            levelUpCounter--;

            if (levelUpCounter < 0)
            {
                LevelUp();
            }
        }

        GatherInput();
        UpdateHourAndMinute();

        timer.text = globalHour + ":" + globalMinute;
	}

    void GatherInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            dirHRS = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dirHRS = -1;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            dirMNS = 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            dirMNS = -1;
        }

        spdHRS = 1;
        spdMNS = 1;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            spdHRS++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            spdHRS--;
        }

        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
        {
            spdMNS++;
        }
        if (Input.GetKey(KeyCode.L))
        {
            spdMNS--;
        }
    }

    void UpdateHourAndMinute()
    {
        switch (spdHRS)
        {
            case 0: break;
            case 1: hrsClock -= Time.deltaTime; break;
            case 2: hrsClockFast -= Time.deltaTime; break;
        }
        switch (spdMNS)
        {
            case 0: break;
            case 1: mnsClock -= Time.deltaTime; break;
            case 2: mnsClockFast -= Time.deltaTime; break;
        }

        if (hrsClock <= 0)
        {
            hrsClock = 1f;
            globalHour += dirHRS;
        }
        if (hrsClockFast <= 0)
        {
            hrsClockFast = 0.2f;
            globalHour += dirHRS;
        }

        if (mnsClock <= 0)
        {
            mnsClock = 0.5f;
            globalMinute += dirMNS * 5;
        }
        if (mnsClockFast <= 0)
        {
            mnsClockFast = 0.1f;
            globalMinute += dirMNS * 5;
        }

        if (globalHour >= 12) { globalHour = 0; }
        if (globalHour <= -1) { globalHour = 11; }

        if (globalMinute >= 60) { globalMinute = 0; }
        if (globalMinute <= -1) { globalMinute = 55; }

    }

    void ArmAndShoot()
    {
        int setHRS;
        int setMNS;

        int i = Random.Range(0, latestHRS.Length + 1);

        if (i < latestHRS.Length)
        {
            setHRS = latestHRS[i];
        }
        else
        {
            setHRS = Random.Range(0, 12);

            for (int j = latestHRS.Length - 1; j > -1; j--)
            {
                if (j == 0)
                {
                    latestHRS[j] = setHRS;
                } else
                {
                    latestHRS[j] = latestHRS[j - 1];
                }
            }
        }

        i = Random.Range(0, latestMNS.Length + 1);

        if (i < latestMNS.Length)
        {
            setMNS = latestMNS[i];
        }
        else
        {
            setMNS = Random.Range(0, 12) * 5;

            for (int j = latestMNS.Length - 1; j > -1; j--)
            {
                if (j == 0)
                {
                    latestMNS[j] = setMNS;
                }
                else
                {
                    latestMNS[j] = latestMNS[j - 1];
                }
            }
        }

        i = Random.Range(0, silos.Length);

        silos[i].LaunchRocket(setHRS, setMNS);
    }

    void LevelUp()
    {
        minDelay -= delayDecrease;
        maxDelay -= delayDecrease;

        if (minDelay < minDelayABS) minDelay = minDelayABS;
        if (maxDelay < maxDelayABS) maxDelay = maxDelayABS;
    }
}
