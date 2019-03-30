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

    private void Start()
    {
        ShowNext();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShowNext();
    }

    public void ShowNext()
    {
        if (nextGameObject - 1 >= 0)
            tutorialObjects[nextGameObject - 1].SetActive(false);

        if (nextGameObject < tutorialObjects.Length)
            tutorialObjects[nextGameObject].SetActive(true);
        else
            StartGame();

        nextGameObject++;
    }

    public void StartGame()
    {
        GM.StartGame();
        Destroy(gameObject);
    }
}
