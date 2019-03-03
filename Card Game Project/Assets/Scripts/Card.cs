using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : Draggable {

    // used for card Attack actions
    public delegate void Attack(Enemy en);

    //[HideInInspector]
    public Attack CardAttack;

    [Header("References")]

    [SerializeField]
    private Deck deck;

    // add player's reference here
    //[SerializeField]
    //private Player player;


    [Header("Card Specific")]

    [Tooltip("the card's damage")]
    public int damage;
    [Tooltip("the card's stamina cost")]
    public int cost;
    [Tooltip("The card's description. To print Stats use statName_STAT")]
    public string[] description;


    [Header("Card References")]

    [SerializeField]
    private TextMeshProUGUI staminaCostUI;

    [SerializeField]
    private TextMeshProUGUI cardDescriptionUI;

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

        CardAttack = DealDamage;
    }

    private void Start()
    {
        UpdateCard();
    }

    public void ToDiscardPile() // send card to discard pile. Currently hard coded
    {
        deck.mDiscard.Add(gameObject);
        gameObject.SetActive(false);
        transform.SetParent(deck.transform.Find("DiscardPile"));
    }

    #region CardEffects

    private void DealDamage(Enemy en) // currently hard coded for only one enemy. Should change that to apply for AOE damage
    {
        en.health -= damage;
    }

    #endregion
  

    #region CardDisplay

    // gonna be used in the inherited class
    protected void UpdateCard()
    {
        UpdateCardCost(); // update card's cost. Cost might change on runtime.
        UpdateCardDescription(); // update card's 'description. Description might change on runtime.
    }

    private void UpdateCardCost()
    {
        if(staminaCostUI == null) // fool prof
        {
            Debug.LogWarning("Couldn't update " + transform.name + "'s cost");
            return;
        }

        staminaCostUI.text = cost.ToString(); // updates description
    }

    private void UpdateCardDescription()
    {
        if (cardDescriptionUI == null) // fool proof
        {
            Debug.LogWarning("Couldn't update " + transform.name + "'s cost");
            return;
        }

        string text = ""; // description placeholder

        for (int i = 0; i < description.Length; i++) // adds all elements of the array to the description
        {
            string addString = ""; // current array string

            switch (description[i]) // checks for variable keywords and adds the proper stat instead
            {
                case "damage_STAT":
                    addString = damage.ToString();
                    break;
                default:
                    addString = description[i];
                    break;
            }

            text += addString + " ";                     
        }

        cardDescriptionUI.text = text;
    }

    #endregion

}
