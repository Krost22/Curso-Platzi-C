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
    public AudioSource soundTrack;
    public static GameManager sharedInstance;
    public PlayerController controller;

    void Awake()
    {
        sharedInstance = this;

        soundTrack = GetComponent<AudioSource>();
        audioPause = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

        controller = GameObject.FindWithTag("Player").
                                GetComponent<PlayerController>();

        SetGameState(GameState.menu);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && count == 2) //Volver al juego 
        {
            SetGameState(GameState.inGame);
            Time.timeScale = 1f;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && count == 1) //Poner Pausa (ir al menu)
        {
            SetGameState(GameState.menu);

            audioPause.Play();

            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //reiniciar
        {
            controller.StartGame();
            SetGameState(GameState.inGame);



        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        Time.timeScale = 1f;
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
           // Time.timeScale = 0f;

            MenuManager.sharedInstance.ShowMainMenu();
            MenuManager.sharedInstance.HideGameOver();

            //TODO: programar logica del menu

        }
        else if(newGameState == GameState.inGame){
            count = 1;

            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();

            controller.Trampa();

            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideGameOver();

        }
        else if (newGameState== GameState.gameOver) 
        {
            count = 0;

            //TODO: preparar cuando se pierde el juego
            MenuManager.sharedInstance.ShowGameOver();

        }

        this.currentGameState = newGameState;
    }
}
