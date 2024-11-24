using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public VisualTreeAsset gameOverAsset;

    private UIDocument uiDocument;
    private List<PlayerController> playerControllers = new List<PlayerController>();
    private int activePlayerController = 0;

    private void Awake()
    {
        uiDocument = GameObject.FindWithTag("MainCamera").GetComponent<UIDocument>();
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in playerGameObjects) {
            playerControllers.Add(go.GetComponent<PlayerController>());
        }
    }

    public void EndGame()
    {
        uiDocument.visualTreeAsset = gameOverAsset;
    }
    public void StartGame()
    {
        uiDocument.visualTreeAsset = gameOverAsset;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                playerControllers[activePlayerController].moveDirectionRequested = context.ReadValue<Vector2>().normalized;
                break;
            default:
                playerControllers[activePlayerController].moveDirectionRequested = Vector2.zero;
                break;
        }
    }

    public void OnSwitchCharacters(InputAction.CallbackContext context)
    {
        playerControllers[activePlayerController].moveDirectionRequested = Vector2.zero;
        activePlayerController = (activePlayerController + 1) % playerControllers.Count;
    }
}
