using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isOccupied;

    private void Update()
    {
        if (Time.frameCount % 5 == 0)
            CheckIfOccupied();
    }

    private void CheckIfOccupied()
    {
        if (transform.childCount > 1)
            isOccupied = true;
        else
            isOccupied = false;
    }
}
