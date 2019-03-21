using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = -1;

    private TextMeshProUGUI scoreText;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        anim = GameObject.Find("All Elements").GetComponent<Animator>();

        TaskCompleted();
    }

    public void TaskCompleted()
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Task Completed: " + score;
    }

    public void PlayerLostScore()
    {
        score--;
        UpdateScore();
        anim.SetTrigger("Shake");
    }

    public int GetScore()
    {
        return score;
    }
}
