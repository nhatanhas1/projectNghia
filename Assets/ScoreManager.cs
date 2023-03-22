using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public Neko2 player;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    int score = 0;
    int highScore = 0;

    public static ScoreManager instance;    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;  
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);  
        highScoreText.text = "High Score :  " + highScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        score = player.score;
        scoreText.text = "Score : " + score.ToString();
        highScoreText.text = "High Score : " + highScore.ToString();
        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}
