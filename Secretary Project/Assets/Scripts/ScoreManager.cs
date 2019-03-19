using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = -1;

    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        TaskCompleted();
    }

    public void TaskCompleted()
    {
        score++;
        scoreText.text = "Task Completed: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}
