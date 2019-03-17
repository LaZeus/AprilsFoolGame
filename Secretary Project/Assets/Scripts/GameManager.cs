using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;

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

    public void UpdateAvailableTasks()
    {
        tasksAvailable = tasksParents.childCount;
        CheckIfLost();
    }

    private void CheckIfLost()
    {
        Debug.Log(tasksAvailable);
        if ( tasksAvailable > 3)
        {
            lives--;

            //

            Debug.Log("Remove the extra task");

            //
            if (lives < 0)
                Debug.Log("Game Over!");
        }
    }
}
