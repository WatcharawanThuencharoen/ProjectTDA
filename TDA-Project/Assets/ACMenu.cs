using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACMenu : MonoBehaviour
{
    public Transform slimeac, goblinac, minoac, clearac;
    // Start is called before the first frame update
    void Start()
    {

        setClear();
    }

    // Update is called once per frame
    void setClear()
    {
        if (StatCatch.totalgoblinkill >= 1)
        {
            slimeac.gameObject.SetActive(true);
        }
        else
        {
            slimeac.gameObject.SetActive(false);
        }
        if (StatCatch.totalslimekill >= 1)
        {
            goblinac.gameObject.SetActive(true);
        }
        else
        {
            goblinac.gameObject.SetActive(false);
        }
        if (StatCatch.totalminokill >= 1)
        {
            minoac.gameObject.SetActive(true);
        }
        else
        {
            minoac.gameObject.SetActive(false);
        }
        if (StatCatch.totalclear >= 1)
        {
            clearac.gameObject.SetActive(true);
        }
        else
        {
            clearac.gameObject.SetActive(false);
        }
    }
}
