using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(0,10)]
    private float taskFrequency;

    [SerializeField]
    private int lives;

    [SerializeField]
    private int tasksAvailable = 0;

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

        nextTaskSlider.maxValue = taskFrequency;

        InvokeRepeating("AddTask", 1, taskFrequency);
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

    public void AddTask()
    {
        startTime = Time.time;
        UpdateAvailableTasks();

        if (tasksAvailable >= 3)       
            CheckIfLost();
        
        else taskGen.GenerateTask();
    }

    private void CheckIfLost()
    {
        lives--;

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
