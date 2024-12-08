using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] const int DEFAULT_POINTS = 1;
    [SerializeField] private TMP_Text scoreTextMesh;

    public static Scorekeeper Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
        score = PersistentData.Instance.getScore();
         UpdateScoreText();
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     score = PersistentData.Instance.getScore();
    //     UpdateScoreText();
    // }

    public void AddPoints(int points = DEFAULT_POINTS)
    {
        score += points;
        PersistentData.Instance.setScore(score);
        UpdateScoreText();
        Debug.Log("score: " + score);
    }

    private void UpdateScoreText()
    {
        scoreTextMesh.text = PersistentData.Instance.getName() + "'s Score: " + score;
    }
}
