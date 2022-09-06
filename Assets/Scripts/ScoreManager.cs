using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textHighScore;
    int score;



    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        textHighScore.text=PlayerPrefs.GetInt("HighScore",0).ToString();
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        textScore.text = score.ToString();

        if(score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            textHighScore.text=score.ToString();
        }

        
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        textHighScore.text = "0";
    }

}