using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFetcher : MonoBehaviour
{

    public Text scoreText;

    void Update()
    {
        scoreText.text = "Final Score: " + GameManagerAndTimer.instance.GetFinalScore();
    }
}