using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;              //Preluam meniul din unity
    public Button LoadButton;
    void Start()
    {
        pauseMenuUI.SetActive(false);           //La inceputul nivelului meniul de pauza este inactiv
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("GameHasBeenSaved") == 1) 
            LoadButton.interactable = true;   //Activam butonul Load doar daca am mai salvat jocul inainte
        if (Input.GetKeyDown(KeyCode.Escape))   //Meniul este activat si dezactivat apasand tasta Escape
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);           //Dezactivam meniul si repornim jocul
        Time.timeScale = 1f;
        GameisPaused = false;
    }
    void Pause()                                    
    {
        pauseMenuUI.SetActive(true);            //Activam meniul si oprim jocul  
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void LoadMainMenu()                  
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}