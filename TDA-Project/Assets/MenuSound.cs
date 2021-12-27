using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public  AudioClip select,click;
    public  AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        select = Resources.Load<AudioClip>("buttonSelect");
        src = GetComponent<AudioSource>();
    }
    public void playSound()
    {
        src.PlayOneShot(select);
    }

    public void stopSound()
    {
        src.Stop();
    }
}
