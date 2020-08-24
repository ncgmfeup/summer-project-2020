using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(optionsMenuUI.activeSelf){
                optionsMenuUI.SetActive(false);
                pauseMenuUI.SetActive(true);
            }

            else if (isPaused){
                Resume();
            }
            else Pause();
        }
    }

    public void Resume(){
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause(){
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Exit(){
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
