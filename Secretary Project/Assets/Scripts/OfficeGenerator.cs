using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeGenerator : MonoBehaviour
{
    public const int maxOfficeLength = 9;

    [SerializeField]
    private int[] roomNumberLimit;

    [SerializeField]
    private GameObject[] possibleRooms;

    private Transform officeParent;

    [SerializeField]
    private int[,] office = new int[maxOfficeLength, maxOfficeLength];

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
        for (int i = 0; i < maxOfficeLength; i++)
            for (int k = 0; k < maxOfficeLength; k++)
                office[i, k] = -1;
    }

    private void GenerateOffice()
    {
        // Office placement
        Vector2 officePosition = new Vector2(Random.Range(0,maxOfficeLength), Random.Range(0, maxOfficeLength));

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
                Vector2 curRoomPos = RandomRoom(prevRoom[0], prevRoom[1]);
            }
            // store room's position in prevRoom
        }
    }

    private void InstantiateOffice()
    {

    }

    private void PrintOffice()
    {
        string printedMessage = "Office: \n";

        for (int i = 0; i < maxOfficeLength; i++)
        {
            string officeCode = " ";

            for (int k = 0; k < maxOfficeLength; k++)
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
        List<int[]> acceptablePlacements = new List<int[]>();
        acceptablePlacements.Add(new int[2]);


        if (acceptablePlacements.Count == 0) return Vector2.zero;

        // return positions
        int pos = Random.Range(0, acceptablePlacements.Count);
        return new Vector2(acceptablePlacements[pos][0], acceptablePlacements[pos][1]);
    }

    private bool IsAcceptableMove(int x, int y)
    {
        if (x < 0 || y < 0 || x > maxOfficeLength - 1 || y > maxOfficeLength - 1)
            return false;

        if (office[x,y] == -1)
        {
            return true;
        }
        return false;
    }

    private bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
}
