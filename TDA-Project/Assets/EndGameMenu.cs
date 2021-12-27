using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public Text text;
    [HideInInspector]
    public GameObject endMenuUI;
    private bool endMenu = false;
    private string endContent;

    void Update()
    {
        if (endMenu)
        {
            ending();
        }
        else if (StatCatch.playerdead || StatCatch.minokilled)
        {
            endMenu = true;
        }
    }
    void ending()
    {
        settext();
        StatCatch.inMenu = true;
        endMenuUI.SetActive(true);
        Time.timeScale = 0f;
        text.text = endContent;
    }
    public void loadMenu()
    {
        
        Time.timeScale = 1f;
        StatCatch.EndGameUpadate();
        StatCatch.inMenu = false;
        endMenuUI.SetActive(false);
        endMenu = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void settext()
    {
        endContent = "Time Play\n" +
            timecount.instance.time + "\n\n" +
            "Goblin Killed\n" + StatCatch.goblinkill + "\n" +
            "Slime Killed\n" + StatCatch.slimekill + "\n" +
            "Total\n" + StatCatch.onegamekill + "\n\n" +
            "Stage Clear\n"+StatCatch.stageCount+"\n\n" +
            "Minotaur Killed\n";
        if (StatCatch.playerdead)
        {
            endContent += "No";
        }
        else if (StatCatch.minokilled)
        {
            endContent += "Yes";
        }
    }
}
