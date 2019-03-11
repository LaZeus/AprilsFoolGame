using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeGenerator : MonoBehaviour
{
    [SerializeField]
    private int[] roomNumberLimit;

    [SerializeField]
    private GameObject[] possibleRooms;

    private Transform officeParent;

    [SerializeField]
    private int[,] office = new int[9, 9];

    // Start is called before the first frame update
    void Start()
    {
        if (officeParent == null)
            officeParent = GameObject.Find("Map").transform;

        CreateOffice();
    }

    public void CreateOffice()
    {
        InitializeOffice();
        GenerateOffice();
        InstantiateOffice();
        PrintOffice();
    }

    private void InitializeOffice()
    {
        for (int i = 0; i < 9; i++)
            for (int k = 0; k < 9; k++)
                office[i, k] = -1;
    }

    private void GenerateOffice()
    {
        // Office placement
        Vector2 officePosition = new Vector2(Random.Range(0,9), Random.Range(0, 9));

        office[(int)officePosition.x, (int)officePosition.y] = 0;

        // Place adjacent
        int numberOfOfficeRooms = Random.Range(roomNumberLimit[0]-1, roomNumberLimit[1]);

        int[] prevRoom = new int[2];
        prevRoom[0] = (int)officePosition.x;
        prevRoom[1] = (int)officePosition.y;

        for (int i = 0; i < numberOfOfficeRooms; i++)
        {
            // generate room
            if (RandomBool()) // attach to office
            {
                Vector2 curRoomPos = RandomRoom((int)officePosition.x, (int)officePosition.y);
            }
            else // attach to last placed room
            {

            }
        }
    }

    private void InstantiateOffice()
    {

    }

    private void PrintOffice()
    {
        string printedMessage = "Office: \n";

        for (int i = 0; i < 9; i++)
        {
            string officeCode = " ";

            for (int k = 0; k < 9; k++)
            {
                if (office[i, k] == -1) officeCode += "- ";
                else officeCode += office[i, k].ToString() + " ";
            }

            printedMessage += officeCode + "\n";
        }
        Debug.Log(printedMessage);
    }

    private Vector2 RandomRoom(int x, int y)
    {
        List<int> accPlacements = new List<int>();



        // return positions
        return new Vector2();
    }

    private bool RandomBool()
    {
        return (Random.value > 0.5f);
    }

}
