using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoSound : MonoBehaviour
{
    public static AudioClip attack1, attack2, dead;
    static AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        attack1 = Resources.Load<AudioClip>("slash");
        attack2 = Resources.Load<AudioClip>("minoAtk");
        dead = Resources.Load<AudioClip>("minoDead");


        src = GetComponent<AudioSource>();
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "attack1":
                src.PlayOneShot(attack1);
                break;
            case "attack2":
                src.PlayOneShot(attack2);
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
