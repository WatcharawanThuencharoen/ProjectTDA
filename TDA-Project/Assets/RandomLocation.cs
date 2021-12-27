using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLocation : MonoBehaviour
{
    private int ranlocation;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.tag == "Player")
        {
            if (StageCount.count < 1)
            {
                ranlocation = Random.Range(0, 4);
            }
            else
            {
                ranlocation = StatCatch.nextLocation;
            }
            
        }
        else
        {
            do
            {
                ranlocation = Random.Range(0, 4);
            }
            while (ranlocation == StatCatch.nextLocation);
        }

        if (ranlocation == 0)
        {
            transform.Translate(-340, 0, 0, Space.World);
        }
        else if (ranlocation == 1)
        {
            transform.Translate(340, 0, 0, Space.World);
        }
        else if (ranlocation == 2)
        {
            transform.Translate(0, -200, 0, Space.World);
        }
        else if (ranlocation == 3)
        {
            transform.Translate(0, 200, 0, Space.World);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
