using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;

    [SerializeField]
    [ContextMenuItem("Get Objects", "GetTutorialObjects")]
    private GameObject[] tutorialObjects;

    [SerializeField]
    private GameObject skipButton;

    private int nextGameObject = 0;

    private void GetTutorialObjects()
    {
        tutorialObjects = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            tutorialObjects[i] = transform.GetChild(i).gameObject;
    }

    public void StartGame()
    {
        GM.StartGame();
        Destroy(gameObject);
    }
}
