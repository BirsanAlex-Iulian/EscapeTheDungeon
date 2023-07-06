using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagScript : MonoBehaviour
{
    private bool ReachedFlag = false;       //Plecam cu ideea ca nu s-a ajuns la steag, adica nivelul nu a fost terminat

    public GameObject FinishMessage;        //Fiind un gameobject public, il putem atribui steagului in editor

    public int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;                      //Retinem scena curenta in care s-a incarcat steagul
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)               //Doar ultimul nivel prezinta un mesaj de sfarsit asa ca folosim conditia if
        {                                                                                  //pentru a evita erori
            FinishMessage.SetActive(false);
        }
    }

    void Update()
    {
        GetComponent<Animator>().Play("Flag");       //Animatia steagului
        if (Input.GetKeyDown(KeyCode.Return) && PlayerPrefs.GetInt("LevelsPassed") == 3) LoadMainMenu();      //Daca am terminat jocul, vom apasa enter pentru a ne intoarce
                                                                                                              //la meniul principal
    }                                                                                                         

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")                        //La coliziunea cu un player redam un sunet care marcheaza ca am trecut nivelul in caz ca steagul nu a mai fost atins
        {
            if (ReachedFlag == false) GetComponent<AudioSource>().Play();
            ReachedFlag = true;                               //si marcam steagul ca atins pentru a evita sa redam sunetul de mai multe ori

            if (currentSceneIndex + 1 != SceneManager.sceneCountInBuildSettings) CallLoadNextScene();   //Daca am atins steagul si urmatorul nivel nu este ultimul
            else                                                                                        //declansam o functie care ne va trimite la urmatorul nivel
            {
                FinishMessage.SetActive(true);
                PlayerPrefs.SetInt("LevelsPassed", 3);                                                  
                Time.timeScale = 0f;                                                                    //Altfel punem pauza jocului,afisam mesajul de sfarsit
                PauseMenuScript.GameisPaused = true;                                                    //si deblocam selectia de nivele
            }
        }
    }

    public void CallLoadNextScene()
    {
        Invoke("LoadNextScene", 0.35f);
        Time.timeScale = 0.35f;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
    }
}
