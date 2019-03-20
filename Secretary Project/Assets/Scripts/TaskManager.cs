using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI person;

    [SerializeField]
    private TextMeshProUGUI room;

    [SerializeField]
    private TextMeshProUGUI details;

    private Transform tasksParent;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDescription("---", " ---", " ---", " ---");
        tasksParent = GameObject.Find("Tasks").transform;
    }

    public void UpdateDescription(string desc, string pers, string ro, string det)
    {
        description.text = desc;
        person.text = "To: " + pers;
        room.text = "Room: " + ro;
        details.text ="Details \n" + det;
    }

    public void DisplayNewTask()
    {
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        yield return null;

        if (tasksParent.childCount > 0)
            tasksParent.GetChild(0).GetComponent<Task>().SendDataToTaskManager();
        else
            UpdateDescription("---", " ---", " ---", " ---");
    }
}
