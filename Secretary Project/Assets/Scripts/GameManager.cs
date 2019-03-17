using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int tasksAvailable = 0;

    private Transform tasksParents;

    // Start is called before the first frame update
    void Start()
    {
        if (tasksParents == null)
            tasksParents = GameObject.Find("Tasks").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateAvailableTasks()
    {
        tasksAvailable = tasksParents.childCount;
    }
}
