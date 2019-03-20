﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : DropZone {

    // add stats here
    // and more stuff

    private GameManager GM;

    private TaskManager taskMan;
    private Transform tasksParent;

	void Awake ()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy

        if (taskMan == null)
            taskMan = FindObjectOfType<TaskManager>().GetComponent<TaskManager>();

        if (tasksParent == null)
            tasksParent = GameObject.Find("Tasks").transform;

        if (GM == null)
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    protected void TaskEffect(Task task)
    {
        StartCoroutine(ActivateCardEffect(task));
    }

    // comment this.
    protected IEnumerator ActivateCardEffect(Task task)
    {
        task.TaskAction?.Invoke();  // perform attack

        yield return null;

        GM.UpdateAvailableTasks();

        task.ToCompletedTasksPile(); // send the just activated card to discard pile

        if (tasksParent.childCount > 0)
            tasksParent.GetChild(0).GetComponent<Task>().SendDataToTaskManager();
        else
            taskMan.UpdateDescription("---", " ---", " ---", " ---");
    }
}
