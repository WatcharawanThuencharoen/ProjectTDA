using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCount : MonoBehaviour
{

    public static int count = 0;
    public Text text;
    public static StageCount instance;
    public string StageName, Name;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void Start()
    {
        count = StatCatch.stageCount;
        mapUpdate();
    }


    void FixedUpdate()
    {

    }
    public void mapUpdate()
    {
        count++;
        StatCatch.inMenu = false;
        name = count+" "+ SceneManager.GetActiveScene().name;
        text.text = name;
        if(count >= 2)
        {
            StatCatch.updateMonsterStat();
            StatCatch.calculateMonsterStat();
        }
    }
}
