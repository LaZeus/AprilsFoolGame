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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateDescription(string desc, string pers, string ro, string det)
    {
        description.text = desc;
        person.text = "To: " + pers;
        room.text = "Room: " + ro;
        details.text ="Details \n" + det;
    }
}
