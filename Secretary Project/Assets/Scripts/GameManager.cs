using System.Collections;
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

    [SerializeField]
    private string[] gameOverText;

    private float startTime;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        if (tasksParents == null)
            tasksParents = GameObject.Find("Tasks").transform;

        if (taskGen == null)
            taskGen = GetComponent<TaskGenerator>();

        if (scoreMan == null)
            scoreMan = GetComponent<ScoreManager>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        anim = GameObject.Find("All Elements").GetComponent<Animator>();

        nextTaskSlider.maxValue = 1;
        taskFrequency = 9.5f;
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
        if (taskFrequency >= 3.5)
            taskFrequency -= 1f;
        else
            taskFrequency = Random.Range(2.5f, 3.5f);


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
            if (i % 2 == 0) audioSource.Play();
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

                int score = GetComponent<ScoreManager>().GetScore();
                string comment = "";
                if (score < 0)
                    comment = "\nHow did you even do that, bro?";
                else
                    comment = "\n" + gameOverText[Random.Range(0, gameOverText.Length)];
                gameOverPanel.transform.Find("ScoreTextEnd").GetComponent<TextMeshProUGUI>().text = "Task Completed: " + score + comment;
            }
            
    }
}
