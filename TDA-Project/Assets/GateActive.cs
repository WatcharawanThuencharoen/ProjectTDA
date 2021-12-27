using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StageCount.count % 3 == 0)
        {
            gameObject.transform.Find("BossGate").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("BossGate").gameObject.SetActive(false);
        }
    }
}
