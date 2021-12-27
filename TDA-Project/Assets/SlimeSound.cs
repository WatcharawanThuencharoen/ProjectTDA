using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSound : MonoBehaviour
{
    public static AudioClip attack, dead;
    public static AudioSource src;
    void Start()
    {
        attack = Resources.Load<AudioClip>("slimeWalk");
        dead = Resources.Load<AudioClip>("slimeDead");


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
