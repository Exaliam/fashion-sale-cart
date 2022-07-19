using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("Error 001: Game Manager is null");
            }

            return instance;
        }
    }

    public GameState State;

    [SerializeField] int stack = 0;
    [SerializeField] int money = 100;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log("Start");
        UpdateGameState(GameState.playing);
    }

    public void UpdateGameState(GameState newState) 
    {
        State = newState;

        switch(newState)
        {
            case GameState.debug:
                //
                break;
            case GameState.gameOver:
                //
                break;
            case GameState.pause:
                //
                break;
            case GameState.playing:
                //
                break;
            case GameState.victory:
                //
                break;
            default:
                Debug.LogError("Error 002: Game State out of range");
                break;
        }
    }
}

public enum GameState
{
    debug,
    playing,
    pause,
    victory,
    gameOver
}
