using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalStagePass : MonoBehaviour
{
    private string randomMap;
    private string[] map = {"Plain", "Forest", "Desert"};
    private Animator anim;
    public GameObject MenuUI;
    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }
    private void Awake()
    {

    }
    private void Update()
    {
        if (MenuUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeMenu();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "PlayerItemCheck" || col.tag == "Player")
        {
            anim.SetBool("Open",true);
            if (Input.GetKeyDown(KeyCode.E))  
            {
                menu();
            }     
        }
    }
    void OnTriggerExit2D()
    {
        anim.SetBool("Open", false);
    }

    void menu()
    {
        MenuUI.SetActive(true);
        StatCatch.inMenu = true;
        Time.timeScale = 0f;
    }
    public void closeMenu()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        StatCatch.inMenu = false;
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
        Time.timeScale = 1f;
    }

}
