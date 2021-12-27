using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowName : MonoBehaviour
{

    public Text setname;

    public void setName(string word)
    {
        setname.text = word;
    }
}
