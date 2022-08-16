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

    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.menu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartGame();
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
            //TODO: programar logica del menu

        }else if(newGameState == GameState.inGame){

            //TODO: preparar la escena para jugar

        }else if (newGameState== GameState.gameOver) 
        {
            //TODO: preparar cuando se pierde el juego
        }

        this.currentGameState = newGameState;
    }
}
