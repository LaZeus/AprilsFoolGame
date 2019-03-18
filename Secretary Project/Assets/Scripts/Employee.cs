using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Stats myStats;

    [SerializeField]
    private Transform mapIcon;

    void Awake()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy

        if (mapIcon == null)
            mapIcon = transform.parent.Find("MapIcon");

        mapIcon.GetComponent<Image>().color = GetComponent<Image>().color;

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
        task.TaskAction?.Invoke();  // perform Action
        task.GiveCoworkerInfo(this);

        yield return null;

        task.ToCompletedTasksPile(); // send the just activated card to discard pile
    }

    public void GoToRoom(Transform room, float timeDelay)
    {
        StartCoroutine(WorkTimer(room, timeDelay));
    }

    private IEnumerator WorkTimer(Transform room,float delay)
    {
        mapIcon.transform.SetParent(room);
        mapIcon.localPosition = Vector3.zero;
        mapIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);
        
        mapIcon.transform.SetParent(transform);
        mapIcon.gameObject.SetActive(false);
        room.transform.GetComponent<Room>().isOccupied = false;
    }

    public void ReturnToBase()
    {
        Debug.Log(transform.name + " now returns to base!");
    }

    private void ShowStats()
    {
        statsPanel.SetActive(true);
    }

    private void HideStats()
    {
        statsPanel.SetActive(false);
    }
}
