using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : DropZone {

    // add stats here
    // and more stuff

    private TaskManager taskMan;
    private Transform tasks;

	void Awake ()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy

        if (taskMan == null)
            taskMan = FindObjectOfType<TaskManager>().GetComponent<TaskManager>();

        if (tasks == null)
            tasks = GameObject.Find("Tasks").transform;
    }

    protected void TaskEffect(Task task)
    {
        StartCoroutine(ActivateCardEffect(task));
    }

    // comment this.
    protected IEnumerator ActivateCardEffect(Task task)
    {
        if(task.TaskAction != null) // fool proof
            task.TaskAction();  // perform attack

        yield return null;

        task.ToCompletedTasksPile(); // send the just activated card to discard pile

        tasks.GetChild(0).GetComponent<Task>().SendDataToTaskManager();
    }
}
