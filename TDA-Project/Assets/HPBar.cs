using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Add UI
public class HPBar : MonoBehaviour
{

    public Slider slider;
    public Text text;
    

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        //slider.value = health;
        text.text = string.Format("{0} / {1}", slider.value, slider.maxValue);
    }
    public void setHealth(int health)
    {
        slider.value = health;
        text.text = string.Format("{0} / {1}", slider.value, slider.maxValue);
    }
}
