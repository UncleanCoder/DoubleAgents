using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public VisualTreeAsset mainMenuAsset;
    public VisualTreeAsset gameOverAsset;
    public VisualTreeAsset gameWonAsset;
    public VisualTreeAsset emptyUiAsset;

    private UIDocument uiDocument;
    private List<PlayerController> playerControllers = new List<PlayerController>();
    private int activePlayerController = 0;
    private GameState gameState = GameState.MainMenu;

    private void Start()
    {
        uiDocument = GameObject.FindWithTag("MainCamera").GetComponent<UIDocument>();
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in playerGameObjects) {
            playerControllers.Add(go.GetComponent<PlayerController>());
        }
        MainMenu();
    }

    private void MainMenu()
    {
        gameState = GameState.MainMenu;
        StopMovement();
        uiDocument.visualTreeAsset = mainMenuAsset;
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        StopMovement();
        uiDocument.visualTreeAsset = gameOverAsset;
    }
    public void GameWon()
    {
        gameState = GameState.GameWon;
        StopMovement();
        uiDocument.visualTreeAsset = gameWonAsset;
    }

    public void StartGame()
    {
        gameState = GameState.GameLoop;
        uiDocument.visualTreeAsset = emptyUiAsset;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (gameState != GameState.GameLoop)
        {
            return;
        }
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
    public void OnActionButton(InputAction.CallbackContext context)
    {
        if (gameState != GameState.GameLoop)
        {
            return;
        }
        if (context.phase == InputActionPhase.Started)
        {
            //TODO
        }
    }

    public void StopMovement()
    {
        playerControllers[activePlayerController].moveDirectionRequested = Vector2.zero;
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
        {
            return;
        }
        switch(gameState)
        {
            case GameState.MainMenu:
                StartGame();
                break;
            case GameState.GameLoop:
                SwitchCharacters();
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                MainMenu();
                break;
        }
    }

    private void SwitchCharacters()
    {
        playerControllers[activePlayerController].moveDirectionRequested = Vector2.zero;
        activePlayerController = (activePlayerController + 1) % playerControllers.Count;
    }
}
