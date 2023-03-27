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
    public int score = 0;
    public int highScore = 0;
    public static ScoreManager instance;        
    void Awake()
    {
        instance = this;  
    } 
    private void Start()
    {     
        highScoreText.text = "High Score :  " + highScore.ToString();
        highScore = PlayerPrefs.GetInt("highScore", highScore);
    }
    // Update is called once per frame
    void Update()
    {
        score = player.score;
        //lấy score từ script của Neko
        scoreText.text = "Score : " + score.ToString();
        //set Text thành score lấy được
        highScoreText.text = "High Score : " + highScore.ToString();
        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore = score;
        }
        //Đổi HighScore nếu score hiện tại lớn hơn highScore đã có.
    }
}
