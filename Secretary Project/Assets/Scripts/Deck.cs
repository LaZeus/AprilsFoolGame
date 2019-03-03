using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    [Header("The Deck Pile")]

    [Tooltip("The Draw Pile Parent")]
    public Transform DeckPile;

    [Tooltip("This is the deck list")]
    public List<GameObject> mDeck = new List<GameObject>();

    [Header("The Discard Pile")]

    [Tooltip("The Discard Pile Parent")]
    public Transform DiscardPile;

    [Tooltip("This is the discard pile")]
    public List<GameObject> mDiscard = new List<GameObject>();


    [Header("The Playable Layouts")]

    [SerializeField]    [Tooltip("The player's hand layout")]
    private LayoutGroup mHand;

    [SerializeField]    [Tooltip("The player's board layout")]
    private LayoutGroup mBoard;

    private void Awake()
    {
        if(mHand == null)
            mHand =GameObject.Find("Hand").GetComponent<LayoutGroup>();

        //mBoard = GameObject.Find("Tabletop").GetComponent<LayoutGroup>();
    }

    public void DrawCard()
    {
        // If one of the piles isn't assigned then do nothing
        if (mDeck == null || mDiscard == null)
            return;

        if (mDeck.Count <= 0)
        {
            if (mDiscard.Count <= 0) // if both piles are empty then do nothing
            {
                Debug.Log("No cards to draw");
                return;
            }

            for (int i = 0; i < mDiscard.Count; i++) // puts every card from the discard pile to the draw pile
            {
                mDiscard[i].transform.SetParent(DeckPile);
                mDeck.Add(mDiscard[i]);
            }

            Shuffle(); // shuffles the draw pile

            mDiscard.Clear(); //clears the discard pile, since you put all the cards in the draw one  
        }

        PutTopDeckCardInHand(); // puts the top card to your hand
    }

    private void PutTopDeckCardInHand() // currently is hard coded
    {
        mDeck[0].transform.SetParent(mHand.transform);
        mDeck[0].SetActive(true);
        mDeck.RemoveAt(0);
    }

    private void Shuffle() // shuffles the draw pile. Currently is hard coded
    {
        for (int i = 0; i < mDeck.Count; i++)
        {
            var temp = mDeck[i];
            int randomIndex = Random.Range(i, mDeck.Count);
            mDeck[i] = mDeck[randomIndex];
            mDeck[randomIndex] = temp;
        }
    }

}
