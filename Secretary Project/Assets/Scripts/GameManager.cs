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

        nextTaskSlider.maxValue = 1;

        Invoke("StartGame",1);
    }

    private void StartGame()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        AddTask();

        //task frequency formula.
        taskFrequency+= 0.5f;


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
        scoreMan.PlayerLostScore();
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
