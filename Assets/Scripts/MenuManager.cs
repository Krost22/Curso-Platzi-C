using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas gameOverCanvas;
    public SpriteRenderer bowser;
    

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }
    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;
        bowser.enabled = true;
    }
    public void HideGameOver()
    {
        gameOverCanvas.enabled = false;
        bowser.enabled = false;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(),
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
