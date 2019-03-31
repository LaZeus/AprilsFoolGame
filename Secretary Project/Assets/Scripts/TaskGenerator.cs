﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskGenerator : MonoBehaviour
{
    private Transform tasksParent;

    public bool gameHasStarted = false;

    [SerializeField]
    private GameObject task;

    [SerializeField]
    private Vector2 timerLimits;

    [Header("Descriptions")]
    [SerializeField]
    private string[] descriptions;

    [Header("Coworkers")]
    [SerializeField]
    [ContextMenuItem("Get Coworkers", "GetAvailableCoworkers")]
    private string[] coworkers;

    [SerializeField]
    private Color[] coworkerColors;

    [Header("Rooms")]
    [SerializeField]
    [ContextMenuItem("Get Rooms", "GetAvailableRooms")]
    private string[] rooms;

    [SerializeField]
    private Sprite[] roomIcons;

    [Header("Details")]
    [SerializeField]
    private string[] details;

    private AudioSource audioSource;

    private void GetAvailableRooms()
    {
        OfficeGenerator oG = GetComponent<OfficeGenerator>();

        rooms = new string[oG.possibleRooms.Length];
        roomIcons = new Sprite[oG.possibleRooms.Length];

        for (int i = 0; i < oG.possibleRooms.Length; i++)
        {
            string[] parts = oG.possibleRooms[i].name.Split(' ');
            rooms[i] = parts[0];
            roomIcons[i] = oG.possibleRooms[i].GetComponent<Image>().sprite;
        }
    }

    private void GetAvailableCoworkers()
    {
        Transform coworkerParent = GameObject.Find("Employees").transform;

        coworkers = new string[coworkerParent.childCount];
        coworkerColors = new Color[coworkerParent.childCount];

        for (int i = 0; i < coworkerParent.childCount; i++)
        {
            coworkers[i] = coworkerParent.GetChild(i).GetComponentInChildren<Employee>().myStats.name;
            coworkerColors[i] = coworkerParent.GetChild(i).GetChild(0).GetComponent<Image>().color;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (tasksParent == null)
            tasksParent = GameObject.Find("Tasks").transform;

        if (audioSource == null)
            audioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void GenerateTask()
    {
        if (tasksParent.childCount < 3 && gameHasStarted)
        {
            GameObject curTask = Instantiate(
                task,
                tasksParent.transform.position,
                tasksParent.rotation,
                tasksParent);

            curTask.name = task.name;          

            Task taskDetails = curTask.GetComponent<Task>();

            // description
            taskDetails.description = descriptions[ReturnRandomIndexFromArray(descriptions)];
            // person
            int curPerson = ReturnRandomIndexFromArray(coworkers);
            taskDetails.person = coworkers[curPerson];
            curTask.GetComponent<Image>().color = coworkerColors[curPerson];
            // room
            int curRoom = ReturnRandomIndexFromArray(rooms);
            taskDetails.room = rooms[curRoom];
            curTask.transform.Find("Art").GetComponent<Image>().sprite = roomIcons[curRoom];

            // details
            taskDetails.details = details[ReturnRandomIndexFromArray(details)];

            taskDetails.timer = Random.Range(timerLimits.x, timerLimits.y);

            if (tasksParent.childCount == 1)
                taskDetails.SendDataToTaskManager();

            audioSource.Play();
        }
    }

    private int ReturnRandomIndexFromArray(string[] pool)
    {
        return Random.Range(0,pool.Length);
    }
}
