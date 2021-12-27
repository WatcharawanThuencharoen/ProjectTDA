using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWalk : MonoBehaviour
{
    public static AudioClip walk;
    public static AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        walk = Resources.Load<AudioClip>("goblinWalk");
        src = GetComponent<AudioSource>();
    }
    public static void playSound()
    {
        src.PlayOneShot(walk);
    }

    public static void stopSound()
    {
        src.Stop();
    }
}
