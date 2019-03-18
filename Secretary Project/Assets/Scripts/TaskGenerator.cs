using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    private Transform tasksParent;

    [SerializeField]
    private GameObject task;

    [Header("Descriptions")]
    [SerializeField]
    private string[] descriptions;

    [Header("Coworkers")]
    [SerializeField]
    private string[] persons;

    [Header("Rooms")]
    [SerializeField]
    [ContextMenuItem("Get Rooms", "GetAvailableRooms")]
    private string[] rooms;

    [Header("Details")]
    [SerializeField]
    private string[] details;

    private void GetAvailableRooms()
    {
        OfficeGenerator oG = GetComponent<OfficeGenerator>();

        rooms = new string[oG.possibleRooms.Length];

        for (int i = 0; i < oG.possibleRooms.Length; i++)
        {
            string[] parts = oG.possibleRooms[i].name.Split(' ');
            rooms[i] = parts[0];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (tasksParent == null)
            tasksParent = GameObject.Find("Tasks").transform;
    } 

    public void GenerateTask()
    {
        GameObject curTask = Instantiate(
            task,
            tasksParent.transform.position,
            tasksParent.rotation,
            tasksParent);

        curTask.name = task.name;
        Task taskDetails = curTask.GetComponent<Task>();

        // description
        taskDetails.description = ReturnRandomArrayElement(descriptions);
        // person
        taskDetails.person = ReturnRandomArrayElement(persons);
        // room
        taskDetails.room = ReturnRandomArrayElement(rooms);
        // details
        taskDetails.details = ReturnRandomArrayElement(details);

        if (tasksParent.childCount == 1)
            taskDetails.SendDataToTaskManager();

    }

    private string ReturnRandomArrayElement(string[] pool)
    {
        return pool[Random.Range(0,pool.Length)];
    }
}
