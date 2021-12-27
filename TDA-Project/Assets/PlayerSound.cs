using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static AudioClip attack, attackBow, dead, collectItem;
    static AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        collectItem = Resources.Load<AudioClip>("CollectItem");
        attack = Resources.Load<AudioClip>("slash");
        //attack = Resources.Load<AudioClip>("attackMino");
        attackBow = Resources.Load<AudioClip>("attackBow");
        dead = Resources.Load<AudioClip>("playerDead");


        src = GetComponent<AudioSource>();
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "attack":
                src.PlayOneShot(attack);
                break;
            case "attackBow":
                src.PlayOneShot(attackBow);
                break;
            case "dead":
                src.PlayOneShot(dead);
                break;
            case "CollectItem":
                src.PlayOneShot(collectItem);
                break;
        }
    }

    public static void stopSound()
    {
        src.Stop();
    }

}
