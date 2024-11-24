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

    public AudioClip gameOver;
    public AudioClip gameWon;

    private MusicManager musicManager;
    private AudioSource fxAudioSource;
    private UIDocument uiDocument;
    private List<PlayerController> playerControllers = new List<PlayerController>();
    private int activePlayerController = 0;
    private GameState gameState = GameState.MainMenu;

    private void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        fxAudioSource = GetComponent<AudioSource>();
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
        musicManager.Disable();
        fxAudioSource.clip = gameOver;
        fxAudioSource.Play();
        StopMovement();
        uiDocument.visualTreeAsset = gameOverAsset;
    }
    public void GameWon()
    {
        gameState = GameState.GameWon;
        musicManager.Disable();
        fxAudioSource.clip = gameWon;
        fxAudioSource.Play();
        StopMovement();
        uiDocument.visualTreeAsset = gameWonAsset;
    }

    public void StartGame()
    {
        gameState = GameState.GameLoop;
        uiDocument.visualTreeAsset = emptyUiAsset;
        StartMovement();
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

    public void StartMovement()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("NPC"))
        {
            go.GetComponent<GuardController>().Enable();
        }
    }

    public void StopMovement()
    {
        playerControllers[activePlayerController].moveDirectionRequested = Vector2.zero;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("NPC"))
        {
            go.GetComponent<GuardController>().Disable();
        }
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

    internal Transform getActivePlayerTransform()
    {
        return playerControllers[activePlayerController].transform;
    }
}
