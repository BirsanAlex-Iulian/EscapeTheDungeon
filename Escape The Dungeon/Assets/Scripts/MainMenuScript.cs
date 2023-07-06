using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button LevelsButton;                 //Cele 2 butoane sunt declarate public ca sa le alocam direct in editorul unity
    public Button LoadButton;

    void Start()                                //Ce se intampla la deschiderea obiectelor care au atasate scriptul MainMenuScript
    {                                           //PlayerPrefs stocheaza preferintele jucatorului intre sesiunile de joc
        if (PlayerPrefs.GetInt("LevelsPassed") == 3)        //Daca am terminat jocul deblocam selectia de nivele
        {
            LevelsButton.interactable = true;
        }
        if (PlayerPrefs.GetInt("GameHasBeenSaved") == 1)    //Daca am salvat inainte, vom debloca butonul Load
        {
            LoadButton.interactable = true;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("LevelsPassed", 0);      //De fiecare data cand apasam butonul startgame resetam preferintele jucatorului
        PlayerPrefs.SetInt("GameHasBeenSaved", 0);
        Time.timeScale = 1f;                        //Time.timeScale gestioneaza viteza jocului si cuprinde valori de la 0 la 1(la 0 este oprit si la 1 este in timp real)
        PauseMenuScript.GameisPaused = false;           //Variabila din scriptul meniului de pauza care gestioneaza daca jocul este sau nu pe pauza
                                                        //este necesara daca pornim meniul principal din meniul de pauza
        SceneManager.LoadScene(1);                      
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    //SaveData reprezinta o clasa in care salvam datele, si SaveLoadSystem reprezinta sistemul prin care le gestionam
    public void LoadGame()
    {
        SaveData data1 = SaveLoadSystem.LoadPlayer();       
        SceneManager.LoadScene(data1.currentLevel);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }
    public void LoadLevel1()                                    
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }
}
