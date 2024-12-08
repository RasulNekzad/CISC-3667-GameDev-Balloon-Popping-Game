using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SaveHighScores : MonoBehaviour
{

    [SerializeField] const int NUM_HIGH_SCORES = 5;
    [SerializeField] const string NAME_KEY = "HighScoreName";
    [SerializeField] const string SCORE_KEY = "HighScore";

    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] TextMeshProUGUI[] nameTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        playerName = PersistentData.Instance.getName();
        playerScore = PersistentData.Instance.getScore();

        SaveScore();
        DisplayHighScores();
        
    }

    private void SaveScore()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;

            { 
                if (PlayerPrefs.HasKey(currentScoreKey))
                {
                    int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                    if (playerScore > currentScore)
                    {
                        //handle this case
                        int tempScore = currentScore;
                        string tempName = PlayerPrefs.GetString(currentNameKey);

                        PlayerPrefs.SetString(currentNameKey, playerName);
                        PlayerPrefs.SetInt(currentScoreKey, playerScore);

                        playerScore = tempScore;
                        playerName = tempName;
                    }

                }
                else
                {
                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);
                    return;
                }
            }
        }
    }

    public void DisplayHighScores()
    {    
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY+i);
            scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY+i).ToString();
        }
    }
    

    
}
