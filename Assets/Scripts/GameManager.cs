using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public VisualTreeAsset gameOverAsset;

    private UIDocument uiDocument;

    private void Start()
    {
        uiDocument = GameObject.FindWithTag("MainCamera").GetComponent<UIDocument>();
    }

    public void EndGame()
    {
        uiDocument.visualTreeAsset = gameOverAsset;
    }
    public void StartGame()
    {
        uiDocument.visualTreeAsset = gameOverAsset;
    }
}
