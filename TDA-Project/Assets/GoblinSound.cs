using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSound : MonoBehaviour
{
    public static AudioClip attack, dead;
    static AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        attack = Resources.Load<AudioClip>("goblinAtk");
        dead = Resources.Load<AudioClip>("goblinDead");


        src = GetComponent<AudioSource>();
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "attack":
                src.PlayOneShot(attack);
                break;
            case "dead":
                src.PlayOneShot(dead);
                break;
        }
    }

    public static void stopSound()
    {
        src.Stop();
    }
}
