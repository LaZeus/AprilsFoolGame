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
        Vector2 officePosition = new Vector2(Random.Range(0, maxOfficeLength), Random.Range(0, maxOfficeLength));

        office[(int)officePosition.x, (int)officePosition.y] = 0;

        // Place adjacent
        int numberOfOfficeRooms = Random.Range(roomNumberLimit[0], roomNumberLimit[1]) - 1;

        List<int[]> rooms = new List<int[]>();

        int[] prevRoom = new int[] { (int)officePosition.x, (int)officePosition.y };

        rooms.Add(prevRoom);

        for (int i = 0; i < numberOfOfficeRooms; i++)
        {
            Vector2 curRoomPos;
            // generate room
            if (RandomBool()) // attach to office
            {
                curRoomPos = RandomRoom((int)officePosition.x, (int)officePosition.y, rooms);
            }
            else // attach to last placed room
            {
                curRoomPos = RandomRoom(rooms[rooms.Count-1][0], rooms[rooms.Count - 1][1], rooms);
            }

            office[(int)curRoomPos.x, (int)curRoomPos.y] = 1;

            // store room's position in prevRoom
            
            prevRoom[0] = (int)curRoomPos.x;
            prevRoom[1] = (int)curRoomPos.y;

            rooms.Add(prevRoom);
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

    private Vector2 RandomRoom(int x, int y, List<int[]> prevRooms)
    {
        //List<int[]> nonCheckedRooms = new List<int[]>();
        int nextRoomToCheck = prevRooms.Count - 1;

        List<int[]> acceptablePlacements = new List<int[]>();

        FindProperRoom(x, y, nextRoomToCheck, ref nextRoomToCheck, ref acceptablePlacements, ref prevRooms);

        // return positions
        int pos = Random.Range(0, acceptablePlacements.Count);
        return new Vector2(acceptablePlacements[pos][0], acceptablePlacements[pos][1]);
    }

    private void FindProperRoom(int x, int y, int curNumber, ref int nextRoomToCheck, ref List<int[]> acceptablePlacements, ref List<int[]> rooms)
    {
        // check for acceptable rooms and assign them to the list
        CheckAdjusentRooms(x, y, out acceptablePlacements);

        //if there are no acceptable rooms
        if (acceptablePlacements.Count == 0)
        {
            nextRoomToCheck--;

            if (nextRoomToCheck < 0)
            {
                Debug.LogWarning("No acceptable moves");
            }

            // recursion
            FindProperRoom(
                rooms[nextRoomToCheck][0], 
                rooms[nextRoomToCheck][1],
                nextRoomToCheck,
                ref nextRoomToCheck,
                ref acceptablePlacements,
                ref rooms);
        }
    }

    private void CheckAdjusentRooms(int x, int y, out List<int[]> acceptablePlacements)
    {
        List<int[]> accPlacements = new List<int[]>();

        for (int i = -1; i < 2; i += 2)
        {
            if (IsAcceptableRoom(x + i, y))
            {
                int[] room = new int[2];
                room[0] = x + i;
                room[1] = y;

                accPlacements.Add(room);
            }
        }
        for (int i = -1; i < 2; i += 2)
        {
            if (IsAcceptableRoom(x, y + i))
            {
                int[] room = new int[2];
                room[0] = x;
                room[1] = y + i;

                accPlacements.Add(room);
            }
        }

        acceptablePlacements = accPlacements;
    }

    private bool IsAcceptableRoom(int x, int y)
    {
        if (x < 0 || y < 0 || x > maxOfficeLength - 1 || y > maxOfficeLength - 1)
            return false; // index out of length

        if (office[x,y] == -1)
            return true; // empty space

        return false; // something already there
    }

    private bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
}
