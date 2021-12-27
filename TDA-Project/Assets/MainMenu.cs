using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text user;
    private string[] map = { "Plain", "Forest", "Desert" };
    private void Start()
    {
        StatCatch.MainMenuReset();
        user.text = "Login with : \n" +  StatCatch.playerProfile;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map[Random.Range(0, 3)]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
