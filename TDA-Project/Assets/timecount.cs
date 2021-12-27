using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timecount : MonoBehaviour
{
    //private float timer = 0;
    public static float Seconds = 0;
    public static int Hours = 0, Minutes = 0;
    public Text text;
    public static timecount instance;
    public string time;

    void Start()
    {
        StatCatch.setTime();
    }

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

    void FixedUpdate()
    {
        timeUpdate();
    }
    private void timeUpdate()
    {
        //timer += Time.deltaTime;
        Seconds += Time.deltaTime;
        time = string.Format("{0}:{1:00}:{2:00}",Hours ,Minutes ,(int)Seconds);
        text.text = time;
        if(Seconds >= 60)
        {
            Minutes++;
            Seconds = Seconds % 60;
        }
        if(Minutes >= 60)
        {
            Hours++;
            Minutes = Minutes % 60;
        }
    }
}
