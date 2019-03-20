using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : Draggable {

    // used for card Attack actions

    public Action TaskAction;

    [Header("References")]

    private GameManager GM;
    [SerializeField]
    private TaskManager TaskMan;
    private Transform map;

    // add player's reference here
    //[SerializeField]
    //private Player player;

    [Header("Task Specific")]

    public string description;
    public string person;
    public string room;
    public string details;

    public float timer;

    // add more stuff here
    // and more comments too

    private void Awake()
    {
        Initialization();
    }

    // gonna be used in the inherited class
    public void Initialization()
    {
        if (TaskMan == null)
            TaskMan = FindObjectOfType<TaskManager>().GetComponent<TaskManager>();

        if (map == null)
            map = GameObject.Find("Map").transform;

        //room = myRoomType.ToString();

        TaskAction -= DisplayText;
        TaskAction += DisplayText;

        onPickActions -= SendDataToTaskManager;
        onPickActions += SendDataToTaskManager;
    }

    private void Start()
    {
        
    }

    public void ToCompletedTasksPile() // send card to discard pile. Currently hard coded
    {
        //deck.mDiscard.Add(gameObject);
        Destroy(gameObject);
        //gameObject.SetActive(false);
        //transform.SetParent(deck.transform.Find("DiscardPile"));
    }

    #region TaskActions

    private void DisplayText() 
    {
        Debug.Log(transform.name + " was moved!");
        TaskMan.DisplayNewTask();
    }

    public void GiveCoworkerInfo(Employee coworker)
    {
        if (coworker.myStats.name == person)
            timer /= 2;

        Debug.Log("Timer is " + timer);
        coworker.GoToRoom(RoomToTransform(), timer);       
    }

    #endregion

    private Transform RoomToTransform()
    {
        List<Transform> sameRooms = new List<Transform>();

        for (int i = 0; i < map.childCount; i++)
        {
            Transform curRoom = map.GetChild(i);
            if (curRoom.name == room + " Variant" && !curRoom.GetComponent<Room>().isOccupied)
                sameRooms.Add(map.GetChild(i));
        }

        Transform returnedRoom = sameRooms[Random.Range(0, sameRooms.Count)];
        returnedRoom.GetComponent<Room>().isOccupied = true;

        return returnedRoom;
    }
  
    #region TaskDisplay

    public void SendDataToTaskManager()
    {
        TaskMan.UpdateDescription(description, person, room, details);
    }
    #endregion

}
