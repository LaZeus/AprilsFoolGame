using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = -1;

    private TextMeshProUGUI scoreText;

    private Animator anim;

    private AudioSource audioSource;
    private ParticleSystem[] mParticles;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        anim = GameObject.Find("All Elements").GetComponent<Animator>();

        GameObject particles = GameObject.Find("Particles");
        audioSource = particles.GetComponent<AudioSource>();
        mParticles = particles.GetComponentsInChildren<ParticleSystem>();

        TaskCompleted();
    }

    public void TaskCompleted()
    {
        score++;

        //particles here
        if (score > 0)
        {
            foreach (ParticleSystem part in mParticles)
                part.Play();
            audioSource.Play();
        }

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
