using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : DropZone {

    // add stats here
    // and more stuff

	void Awake ()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy
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
    }
}
