using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler {

    protected delegate void Actions(Task c);

    // used to check card stuff
    [Header("Card Group Aspects")]
    [Tooltip("Accepts cards of this type")]
    public Draggable.CardType typeOfCard = Draggable.CardType.All;

    // when card is dropped actions
    protected Actions OnDropActions;

    private Task task = null;

    public void OnPointerEnter(PointerEventData eventData) // IPointerEnterHandler
    {
        // add glow effect on Card

        if (eventData.pointerDrag == null)
            return;

        task = eventData.pointerDrag.GetComponent<Task>();

        // store the card's origin parent(i.e. hand or playarea)
        if (task != null)
            if (typeOfCard == task.mCardType || typeOfCard == Draggable.CardType.All)
                task.placeholderParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData) // IPointerExitHandler
    {
        // stop glow effect on Card

        if (eventData.pointerDrag == null)
            return;

        task = eventData.pointerDrag.GetComponent<Task>();

        // if the card is out of a playable area then set that the card will return to the original position
        if (task != null && task.placeholderParent == transform)
            if (typeOfCard == task.mCardType || typeOfCard == Draggable.CardType.All)
                task.placeholderParent = task.parentToReturnTo;
    }

    public void OnDrop(PointerEventData eventData) // IDropHandler
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        task = eventData.pointerDrag.GetComponent<Task>();

        // includes the card in the proper layout group(i.e. hand)
        if (task != null)
        {
            if (typeOfCard == task.mCardType || typeOfCard == Draggable.CardType.All)
            {
                task.parentToReturnTo = transform;
                if (OnDropActions != null)
                    OnDropActions(task);
            }
        }

    }
}
