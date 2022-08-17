using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState  //Me ayuda a saber en que estado está actualmente el juego
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    int count;

    public GameState currentGameState;
    public AudioSource audioPause;
    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
        audioPause = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.menu);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && count == 2)
        {
            SetGameState(GameState.inGame);
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && count == 1)
        {
            SetGameState(GameState.menu);
            Time.timeScale = 0f;
            audioPause.Play();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        
    }

    public void BackToMenu()//Se ejecuta cuando el usuario quiera volver al menu principal
    {
        SetGameState(GameState.menu);
        
    }

    

    public void GameOver()//Se ejecuta cuando la vida llega a 0
    {
        SetGameState(GameState.gameOver);
        
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            count = 2;
            //TODO: programar logica del menu

        }else if(newGameState == GameState.inGame){
            count = 1;

            //TODO: preparar la escena para jugar

        }else if (newGameState== GameState.gameOver) 
        {
            count = 0;
            //TODO: preparar cuando se pierde el juego
        }

        this.currentGameState = newGameState;
    }
}
