using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : Draggable {

    // used for card Attack actions

    public Action TaskAction;

    [Header("References")]

    [SerializeField]
    private Deck deck;

    private TaskManager TaskMan;

    // add player's reference here
    //[SerializeField]
    //private Player player;

    [Header("Task Specific")]

    public string description;
    public string person;
    public string room;
    public string details;

    // add more stuff here
    // and more comments too

    private void Awake()
    {
        Initialization();
    }

    // gonna be used in the inherited class
    protected void Initialization()
    {
        if (deck == null)
            deck = GameObject.Find("Deck").GetComponent<Deck>();

        if (TaskMan == null)
            TaskMan = FindObjectOfType<TaskManager>().GetComponent<TaskManager>();

        TaskAction -= DisplayText;
        TaskAction += DisplayText;

        onPickActions -= SendDataToTaskManager;
        onPickActions += SendDataToTaskManager;
    }

    private void Start()
    {
        //UpdateCard();
    }

    public void ToCompletedTasksPile() // send card to discard pile. Currently hard coded
    {
        deck.mDiscard.Add(gameObject);
        gameObject.SetActive(false);
        transform.SetParent(deck.transform.Find("DiscardPile"));
    }

    #region TaskActions

    private void DisplayText() 
    {
        Debug.Log(transform.name + " was moved!");
    }

    public void GiveCoworkerInfo(Employee coworker)
    {
        Transform tempRoom = GameObject.Find("Map").transform.GetChild(0);
        coworker.GoToRoom(tempRoom);
    }

    #endregion
  
    #region TaskDisplay

    public void SendDataToTaskManager()
    {
        TaskMan.UpdateDescription(description, person, room, description);
    }

    /*private void UpdateCardDescription()
    {      
        string text = ""; // description placeholder

        for (int i = 0; i < description.Length; i++) // adds all elements of the array to the description
        {
            string addString = ""; // current array string

            switch (description[i]) // checks for variable keywords and adds the proper stat instead
            {
                case "damage_STAT":
                    addString = title;
                    break;
                default:
                    addString = description[i];
                    break;
            }

            text += addString + " ";                     
        }

        //cardDescriptionUI.text = text;
    }*/

    #endregion

}
