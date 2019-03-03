using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutGroup))]
public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler {

    protected delegate void Actions(Card c);

    // used to check card stuff
    [Header("Card Group Aspects")]
    [Tooltip("Accepts cards of this type")]
    public Card.CardType typeOfCard = Card.CardType.All;

    // when card is dropped actions
    protected Actions OnDropActions;

    private Card card = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // add glow effect on Card

        if (eventData.pointerDrag == null)
            return;

        card = eventData.pointerDrag.GetComponent<Card>();

        // store the card's origin parent(i.e. hand or playarea)
        if (card != null)
            if (typeOfCard == card.mCardType || typeOfCard == Card.CardType.All)
                card.placeholderParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // stop glow effect on Card

        if (eventData.pointerDrag == null)
            return;

        card = eventData.pointerDrag.GetComponent<Card>();

        // if the card is out of a playable area then set that the card will return to the original position
        if (card != null && card.placeholderParent == transform)
            if (typeOfCard == card.mCardType || typeOfCard == Card.CardType.All)
                card.placeholderParent = card.parentToReturnTo;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        card = eventData.pointerDrag.GetComponent<Card>();

        // includes the card in the proper layout group(i.e. hand)
        if (card != null)
        {
            if (typeOfCard == card.mCardType || typeOfCard == Draggable.CardType.All)
            {
                card.parentToReturnTo = transform;
                if (OnDropActions != null)
                    OnDropActions(card);
            }
        }

    }
}
