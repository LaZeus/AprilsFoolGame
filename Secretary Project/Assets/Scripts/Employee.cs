using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Employee : DropZone
{
    [SerializeField]
    private GameObject statsPanel;

    // add stats here
    // and more stuff
    [System.Serializable]
    public struct Stats
    {
        public string name;
        public string gender;
        public string age;
        public string habits;
    }

    [SerializeField]
    private Stats myStats;

    void Awake()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy

        onPointerEnterActions -= ShowStats;
        onPointerEnterActions += ShowStats;

        onPointerExitActions -= HideStats;
        onPointerExitActions += HideStats;

        statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + myStats.name;
        statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Gender: " + myStats.gender;
        statsPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Age: " + myStats.age;
        statsPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Habits: " + myStats.habits;
    }

    protected void TaskEffect(Task task)
    {
        StartCoroutine(ActivateCardEffect(task));
    }

    // comment this.
    protected IEnumerator ActivateCardEffect(Task task)
    {
        if (task.TaskAction != null) // fool proof
            task.TaskAction();  // perform attack

        yield return null;

        task.ToCompletedTasksPile(); // send the just activated card to discard pile
    }

    public void ReturnToBase()
    {
        Debug.Log(transform.name + " now returns to base!");
    }

    private void ShowStats()
    {
        Debug.Log("Show stats");
        statsPanel.SetActive(true);
    }

    private void HideStats()
    {
        Debug.Log("Hide stats");
        statsPanel.SetActive(false);
    }
}
