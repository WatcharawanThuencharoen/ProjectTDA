using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMenu : MonoBehaviour
{
    public GameObject MenuUI;
    private string randomMap;
    private string[] map = { "Plain", "Forest", "Desert" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeMenu()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void nextStage()
    {
        StatCatch.EndStageUpdate();
        do
        {
            randomMap = map[Random.Range(0, 3)];
        }
        while (randomMap == SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(randomMap);
    }
    public void bossStage()
    {
        StatCatch.EndStageUpdate();
        SceneManager.LoadScene("Cave");
    }
}
