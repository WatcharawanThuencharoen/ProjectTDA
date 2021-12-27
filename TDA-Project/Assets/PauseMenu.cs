using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                resume();
                StatCatch.inMenu = false;
            }
            else
            {
                pause();
                StatCatch.inMenu = true;
            }
        }   
        
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        isPause = true;
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
        isPause = false;
    }

    public void loadMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        resume();
        StatCatch.totalplay++;
        StatCatch.playerdead = true;
    }
}
