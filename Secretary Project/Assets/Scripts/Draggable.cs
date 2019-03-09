using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    // used for card stuff
    public enum TaskType {Attack, Escape, All};

    public delegate void Action();

    protected Action onPickActions;

    [Header("Task Specific")]
    [Tooltip("CARD CAN'T BE OF TYPE 'ALL'")]
    public TaskType mCardType = TaskType.Attack;

    [Header("Task Management")]
    [Tooltip("Allows the user to change the cards order during runtime")]
    public bool changeTaskOrder;

    [HideInInspector]
    public Transform parentToReturnTo = null;
    [HideInInspector]
    public Transform placeholderParent = null;


    [Header("Required Components")]
    [SerializeField]
    private CanvasGroup mCanvasGrp;

    [SerializeField]
    private LayoutElement mLayoutElement;

    //private Vector2 offset; // will store the offeset between mouse and card 

    private GameObject placeholder = null; // placeholder for the card you are moving

    public void OnBeginDrag(PointerEventData eventData)
    {
        onPickActions?.Invoke();

        //offset = (Vector2)transform.position - eventData.position;
        CreatePlaceholder();

        // turn off raycasts so you can select the dropzone instead of the card
        mCanvasGrp.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // card follows mouse pointer
        transform.position = eventData.position; //+ offset

        ChangeTasksPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // you go into the dropzone
        transform.SetParent(parentToReturnTo);

        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        // turn back on raycasts since you aren't holding the card anymore
        mCanvasGrp.blocksRaycasts = true;

        // you don't need the placeholder since you placed the card
        Destroy(placeholder);

        ///
        /// List<RaycastResult> results = new List<RaycastResult>();
        /// EventSystem.current.RaycastAll(eventData, results);
        /// 
        /// raycasts and get a list of all things that are underneath
        /// in the list "results"
        /// and you can choose your attack target
        ///
    }

    private void CreatePlaceholder()
    {
        // placeholder instantiation and settings
        placeholder = new GameObject();
        placeholder.transform.name = "Placeholder";
        placeholder.transform.SetParent(transform.parent);

        RectTransform rect = placeholder.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(mLayoutElement.preferredWidth, mLayoutElement.preferredHeight);

        // sets placeholder order in dropzone
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        // removes it from the current Dropzone and stores that dropzone
        parentToReturnTo = transform.parent;
        placeholderParent = parentToReturnTo;
        transform.SetParent(transform.parent.parent);
    }

    private void ChangeTasksPosition()
    {
        //<Change Card Order>

        if (changeTaskOrder)
        {
            if (placeholder.transform.parent != placeholderParent)
                placeholder.transform.SetParent(placeholderParent);

            int newSiblingIndex = placeholderParent.childCount;

            // Sort the card
            for (int i = 0; i < placeholderParent.childCount; i++)
            {
                if (transform.position.x < placeholderParent.GetChild(i).position.x)
                {
                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }
            placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }

        //</Change Card Order>
    }

}
