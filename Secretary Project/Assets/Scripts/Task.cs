using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : Draggable {

    // used for card Attack actions
    public delegate void Action();

    public Action TaskAction;

    [Header("References")]

    [SerializeField]
    private Deck deck;

    // add player's reference here
    //[SerializeField]
    //private Player player;

    [Header("Card Specific")]

    [Tooltip("the card's damage")]
    public string title;
    [Tooltip("the card's stamina cost")]
    public string cost;
    [Tooltip("The card's description. To print Stats use statName_STAT")]
    public string[] description;

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

        TaskAction = DisplayText;
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

    #endregion
  

    #region TaskDisplay

    private void UpdateCardDescription()
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
    }

    #endregion

}
