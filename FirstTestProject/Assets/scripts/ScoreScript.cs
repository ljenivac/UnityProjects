using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    private int highScore = 0;
    private int minScore = 50;

    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        scoreValue = 0;

        //PlayerPrefs.SetInt("HighScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if(player != null)
        {
            int life = player.GetComponent<HealthScript>().hp;
            score.text = "Life: " + life + "\nScore: " + scoreValue + "\n HighScore: " + highScore;

            if(scoreValue > highScore && scoreValue > minScore)
                SceneManager.LoadScene("WinScreen");

            if (scoreValue > highScore)
            {
                highScore = scoreValue;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
        else
            SceneManager.LoadScene("LoseScreen");

    }
}
