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

    private ScoreManager scoreMan;

    private AudioSource audioSource;

    private int jobDone = 0; 

    void Awake()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = TaskEffect; // when a card is placed on enemy

        if (mapIcon == null)
            mapIcon = transform.parent.Find("MapIcon");

        if (scoreMan == null)
            scoreMan = FindObjectOfType<ScoreManager>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        transform.parent.Find("NameField").GetComponentInChildren<TextMeshProUGUI>().text = myStats.name;

        mapIcon.GetComponent<Image>().color = GetComponent<Image>().color;

        onPointerEnterActions -= ShowStats;
        onPointerEnterActions += ShowStats;

        onPointerExitActions -= HideStats;
        onPointerExitActions += HideStats;

        Transform statsText = statsPanel.transform.Find("StatsText");
        statsText.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + myStats.name;
        statsText.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Gender: " + myStats.gender;
        statsText.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Age: " + myStats.age;
        statsText.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Habits: " + myStats.habits;
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
        audioSource.Play();
        int curJob = jobDone;
        mapIcon.transform.SetParent(room);
        mapIcon.localPosition = Vector3.zero;
        mapIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        if (curJob == jobDone)
        {
            jobDone++;
            mapIcon.transform.SetParent(transform);
            mapIcon.gameObject.SetActive(false);
            room.transform.GetComponent<Room>().isOccupied = false;
            // Spawn particles
            scoreMan.TaskCompleted();
        }
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
