using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAndTimer : MonoBehaviour {

    public int globalHour;
    public int globalMinute;
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
