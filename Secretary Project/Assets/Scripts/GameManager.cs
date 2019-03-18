using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;

    [SerializeField]
    private int tasksAvailable = 0;

    private Transform tasksParents;

    private TaskGenerator taskGen;

    // Start is called before the first frame update
    void Start()
    {
        if (tasksParents == null)
            tasksParents = GameObject.Find("Tasks").transform;

        if (taskGen == null)
            taskGen = GetComponent<TaskGenerator>();
    }

    public void UpdateAvailableTasks()
    {
        tasksAvailable = tasksParents.childCount;       
    }

    public void AddTask()
    {
        UpdateAvailableTasks();

        if (tasksAvailable >= 3)
        {
            Debug.Log("Lose life here");
            CheckIfLost();
        }
        else taskGen.GenerateTask();
    }

    private void CheckIfLost()
    {
        lives--;

        //

        Debug.Log("Lost Life");

        //
        if (lives < 0)
            Debug.Log("Game Over!");

    }
}
