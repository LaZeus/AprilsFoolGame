﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float taskFrequency;

    private Animator anim;

    [SerializeField]
    private int lives;

    [SerializeField]
    private int tasksAvailable = 0;

    private ScoreManager scoreMan;

    private Transform tasksParents;

    private TaskGenerator taskGen;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Slider nextTaskSlider;

    private float startTime;

    // Start is called before the first frame update
    private void Start()
    {
        if (tasksParents == null)
            tasksParents = GameObject.Find("Tasks").transform;

        if (taskGen == null)
            taskGen = GetComponent<TaskGenerator>();

        if (scoreMan == null)
            scoreMan = GetComponent<ScoreManager>();

        anim = GameObject.Find("All Elements").GetComponent<Animator>();

        nextTaskSlider.maxValue = 1;
        taskFrequency = 10;
        Invoke("StartGame", 1);
    }

    private void StartGame()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        AddTask();

        //task frequency formula.
        if (taskFrequency >= 8.5)
            taskFrequency -= 0.5f;
        else if (taskFrequency >= 7.5)
            taskFrequency -= 0.4f;
        else if (taskFrequency >= 6.5)
            taskFrequency -= 0.3f;
        else if (taskFrequency >= 6)
            taskFrequency -= 0.25f;
        else
            taskFrequency = 5;


        startTime = Time.time;
        nextTaskSlider.maxValue = taskFrequency;

        yield return new WaitForSeconds(taskFrequency);      

        StartGame();
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {       
        nextTaskSlider.value = Time.time - startTime;
    }

    public void UpdateAvailableTasks()
    {
        tasksAvailable = tasksParents.childCount;       
    }

    public void TaskToTrash()
    {
        UpdateAvailableTasks();
        StartCoroutine(TemporaryMoreTasks());
        //scoreMan.PlayerLostScore();
    }

    private IEnumerator TemporaryMoreTasks()
    {
        taskFrequency--;
        for (int i = 0; i < 6; i++)
        {
            anim.SetTrigger("MildShake");
            yield return new WaitForSeconds(1.5f);
        }        
        taskFrequency++;
    }

    public void AddTask()
    {        
        UpdateAvailableTasks();

        if (tasksAvailable >= 3)       
            CheckIfLost();
        
        else taskGen.GenerateTask();
    }

    private void CheckIfLost()
    {
        lives--;
        scoreMan.PlayerLostScore();

        //

        Debug.Log("Lost Life");

        //
        if (lives < 0)
            if (gameOverPanel.activeInHierarchy == false)
            {
                gameOverPanel.SetActive(true);
                gameOverPanel.transform.Find("ScoreTextEnd").GetComponent<TextMeshProUGUI>().text = "Task Completed: " + GetComponent<ScoreManager>().GetScore();
            }
            
    }
}
