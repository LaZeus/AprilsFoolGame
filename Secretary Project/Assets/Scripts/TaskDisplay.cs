using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Task task = null;

    public void OnPointerEnter(PointerEventData eventData) // IPointerEnterHandler
    {
        // add glow effect on Card

        if (eventData.pointerEnter == null)
        {
            Debug.Log("returned");
            return;
        }

        task = eventData.pointerEnter.GetComponent<Task>();

        if (task != null)
        {
            Debug.Log("Show me the fricking stats");
            task.SendDataToTaskManager();
        }

    }

    public void OnPointerExit(PointerEventData eventData) // IPointerExitHandler
    {
        // stop glow effect on Card

        if (eventData.pointerDrag == null)
        {
            return;
        }

        //task = eventData.pointerDrag.GetComponent<Task>();

        // if the card is out of a playable area then set that the card will return to the original position
        //if (task != null && task.placeholderParent == transform)
        // if (typeOfTask == task.myRoomType || typeOfTask == Draggable.TaskType.All)
        //   task.placeholderParent = task.parentToReturnTo;
    }

}

