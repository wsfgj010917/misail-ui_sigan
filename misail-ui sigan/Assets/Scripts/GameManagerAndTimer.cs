using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAndTimer : MonoBehaviour {

    public int globalHour;
    public int globalMinute;
    public Text timer;
    public Image[] hrspdimages;
    public Image[] mnspdimages;

    int dirHRS;
    int spdHRS;
    int dirMNS;
    int spdMNS;

    float hrsClock;
    float hrsClockFast;
    float mnsClock;
    float mnsClockFast;

    public CitySilo[] silos;
    public CityLife[] lives;

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
    public int poitsPerHit;
    int score;
    int combo;
    int scoremultiplier;
    public Text scoreText;
    public Text multiplierText;

    bool gameOver;
    public GameObject inGameStuff;
    public GameObject gameOverStuff;

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

        score = 0;
        combo = 0;
        scoremultiplier = 1;

        gameOver = false;
    }

    
    void Update ()
    {
        if (!gameOver)
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

            UpdateScore();
        }
        if (gameOver)
        {
            inGameStuff.SetActive(false);
            gameOverStuff.SetActive(true);
        }
	}

    void UpdateScore()
    {
        scoreText.text = "Score: " + (score + combo);
        multiplierText.text = "x" + scoremultiplier;
    }

    public void Success()
    {
        combo += poitsPerHit;
        scoremultiplier++;
    }

    public void ComboBreak()
    {
        score += combo * scoremultiplier;
        combo = 0;
        scoremultiplier = 1;

        CheckLives();
    }

    void CheckLives()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (!lives[i].isDead) return;
        }

        gameOver = true;
    }

    public int GetFinalScore()
    {
        return score;
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
        for (int x = 0; x < hrspdimages.Length; x++)
        {
            hrspdimages[x].enabled = false;
        }
        for (int x = 0; x < mnspdimages.Length; x++)
        {
            mnspdimages[x].enabled = false;
        }

        int offsetHRS = 0;
        int offsetMNS = 0;

        if (dirHRS == -1 && spdHRS != 0) offsetHRS = 2;
        if (dirMNS == -1 && spdMNS != 0) offsetMNS = 2;

        hrspdimages[spdHRS + offsetHRS].enabled = true;
        mnspdimages[spdMNS + offsetMNS].enabled = true;

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
