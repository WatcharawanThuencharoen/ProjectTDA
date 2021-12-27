using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour
{

    public Text text;
    public static KillCount instance;
    public string word;
    public int count = 0;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setCount()
    {
        text.text = string.Format("{0} {1} {2}" ,"Eliminated :", count.ToString(),"/ 10");
    }
}
