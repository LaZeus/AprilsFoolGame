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

    [SerializeField]
    List<int[]> rooms = new List<int[]>();

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

        rooms.Clear();
    }

    private void GenerateOffice()
    {
        // Office placement
        Vector2 officePosition = new Vector2(
            Random.Range(maxOfficeLength / 3, maxOfficeLength * 2 / 3), 
            Random.Range(maxOfficeLength / 3, maxOfficeLength * 2 / 3));

        office[(int)officePosition.x, (int)officePosition.y] = 0;

        // Place adjacent
        int numberOfOfficeRooms = Random.Range(roomNumberLimit[0], roomNumberLimit[1]) - 1;

        //List<int[]> rooms = new List<int[]>();
        List<int[]> acceptableRooms = new List<int[]>();

        int[] officeRoom = new int[] { (int)officePosition.x, (int)officePosition.y };

        rooms.Add(officeRoom);
        acceptableRooms.Add(officeRoom);

        for (int i = 0; i < numberOfOfficeRooms; i++)
        {
            Vector2 curRoomPos;

            // Add acceptable rooms here
            UpdateAcceptableRooms(ref acceptableRooms);

            if (RandomBool() && acceptableRooms.Contains(new int[] { (int)officePosition.x, (int)officePosition.y })) // attach to office
            {
                curRoomPos = RandomRoom((int)officePosition.x, (int)officePosition.y);
            }
            else // attach to a random room that accepts more stuff
            {
                int randomRoom = Random.Range(0, acceptableRooms.Count);
                curRoomPos = RandomRoom(acceptableRooms[randomRoom][0], acceptableRooms[randomRoom][1]);
            }

            office[(int)curRoomPos.x, (int)curRoomPos.y] = 1;

            // store room's position in prevRoom
            int[] curRoom = new int[] { (int)curRoomPos.x, (int)curRoomPos.y };

            rooms.Add(curRoom);
            acceptableRooms.Add(curRoom);
        }
    }   

    private void InstantiateOffice()
    {
        for (int i = 0; i < officeParent.childCount; i++)
        {
            Destroy(officeParent.GetChild(i).gameObject);
        }

        int offset = 60;
        for (int i = 0; i < rooms.Count; i++)
        {
            // depending on the room's code it will spawn the equivalent of that list
            GameObject roomPrefab = possibleRooms[office[rooms[i][0], rooms[i][1]]];

            GameObject go = Instantiate(
                roomPrefab,
                officeParent.transform.position + 
                Vector3.down * offset * (rooms[i][0] - maxOfficeLength/2) +
                Vector3.right * offset * (rooms[i][1] - maxOfficeLength / 2),
                officeParent.transform.rotation,
                officeParent);

        }
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

    private void UpdateAcceptableRooms(ref List<int[]> acceptableRooms)
    {
        for (int k = 0; k < acceptableRooms.Count; k++)
        {
            List<int[]> acceptablePlacements = new List<int[]>();

            FindProperRoom(acceptableRooms[k][0], acceptableRooms[k][1], ref acceptablePlacements);

            if (acceptablePlacements.Count == 0)
                acceptableRooms.RemoveAt(k);
        }
    }

    private Vector2 RandomRoom(int x, int y)
    {
        List<int[]> acceptablePlacements = new List<int[]>();

        FindProperRoom(x, y, ref acceptablePlacements);

        // return positions
        int pos = Random.Range(0, acceptablePlacements.Count);

        if (pos < 0) Debug.LogWarning("Pos is negative");

        return new Vector2(acceptablePlacements[pos][0], acceptablePlacements[pos][1]);
    }

    private void FindProperRoom(int x, int y, ref List<int[]> acceptablePlacements)
    {
        // check for acceptable rooms and assign them to the list
        CheckAdjusentRooms(x, y, ref acceptablePlacements);
    }

    private void CheckAdjusentRooms(int x, int y, ref List<int[]> acceptablePlacements)
    {
        CheckLeftRight(x, y, ref acceptablePlacements);
        CheckUpDown(x, y, ref acceptablePlacements);
    }

    private void CheckUpDown(int x, int y, ref List<int[]> acceptablePlacements)
    {
        for (int i = -1; i < 2; i += 2)
            if (IsAcceptableRoom(x, y + i))           
                acceptablePlacements.Add(new int[] { x , y + i});                  
    }

    private void CheckLeftRight(int x, int y, ref List<int[]> acceptablePlacements)
    {
        for (int i = -1; i < 2; i += 2)
            if (IsAcceptableRoom(x + i, y))
                acceptablePlacements.Add(new int[] { x + i, y });
    }

    private bool IsAcceptableRoom(int x, int y)
    {
        if (x < 0 || y < 0 || x >= maxOfficeLength || y >= maxOfficeLength)
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
