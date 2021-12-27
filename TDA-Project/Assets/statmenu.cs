using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statmenu : MonoBehaviour
{
    public Text textField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatetext()
    {
            textField.text = "Lowest time play :" + string.Format("{0}:{1:00}:{2:00}", StatCatch.lhr, StatCatch.lmin, StatCatch.lsec) + "\n" +
            "Highest time play :" + string.Format("{0}:{1:00}:{2:00}", StatCatch.hhr, StatCatch.hmin, StatCatch.hsec) + "\n" +
            "\n" +
            "Highest stage cleared :" + StatCatch.moststageclear + "\n" +
            "\n" +
            "Most enemy killed in one game :" + StatCatch.mostonegamekill + "\n" +
            "Most Goblin killed :" + StatCatch.mostgoblinkill + "\n" +
            "Total Goblin killed :" + StatCatch.totalgoblinkill + "\n" +
            "Most Slime killed :" + StatCatch.mostslimekill + "\n" +
            "Total Slime killed :" + StatCatch.totalslimekill + "\n" +
            "Total Minotaur killed :" + StatCatch.totalminokill + "\n" +
            "\n" +
            "Total cleared :" + StatCatch.totalclear + "\n" +
            "Total play : " + StatCatch.totalplay
            ;
    }
}
