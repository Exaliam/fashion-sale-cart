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

    [Header("Game")]
    public GameState State;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject ground;

    [Header("Debug")] //these values actually must be set by code every scene
    [SerializeField] int stack = 0;
    [SerializeField] int money = 100;
    [SerializeField] [Range(5.0f, 15.0f)] float groundSpeed; 

    bool moveGround;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.playing);
        SpawnPlayer();
    }

    void Update()
    {
        if(moveGround)
        {
            MoveGround();
        }
    }

    public void UpdateGameState(GameState newState) 
    {
        State = newState;

        switch(newState)
        {
            case GameState.debug:
                moveGround = true;
                break;
            case GameState.gameOver:
                moveGround = false;
                break;
            case GameState.pause:
                moveGround = false;
                break;
            case GameState.playing:
                moveGround = true;
                break;
            case GameState.victory:
                moveGround = false;
                WinGame();
                break;
            default:
                moveGround = false;
                Debug.LogError("Error 002: Game State out of range");
                break;
        }
    }

    public void CollectCoin(int coinValue)
    {
        money += coinValue;
        Debug.Log("Added " + coinValue + " coins, now your budget is " + money + " coins");
    }

    public void StackClothes(Color clothesColor, int coinValue)
    {
        stack++;
        money -= coinValue;
        Debug.Log("Added item to stack, now there are " + stack + " clothes in your cart");
        Debug.Log("Your purchase costs " + coinValue + " coins; there are " + money + " coins left in your wallet");
        //Create a function that stacks visually clothes inside the cart
    }

    void SpawnPlayer()
    {
        GameObject playerInstance = Instantiate(playerPrefab, transform.position, Quaternion.Euler(0, 180, 0));
    }

    void MoveGround()
    {
        ground.transform.position += new Vector3(0, 0, -groundSpeed * Time.deltaTime);
    }
    

    void WinGame()
    {
        Debug.Log("Victory");
        Debug.Log("Products purchased:" + stack);
        Debug.Log("Coins left:" + money);
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
