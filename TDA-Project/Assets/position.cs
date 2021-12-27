using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class position : MonoBehaviour
{
    public int sortingOrder = 0;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int mul = -10;
        if (gameObject.layer == 11)
        {
            sortingOrder = (int)(this.transform.position.y * mul) + 266;
        }
        else
        {
            sortingOrder = (int)(this.transform.position.y * mul);
        }
        if (sprite)
            sprite.sortingOrder = sortingOrder;
    }
    //this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().la = (int)this.transform.position.y;
}
