using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCheck : MonoBehaviour
{
    private Animator animator;
    public float atkRate = 0.3f;
    public float nextAtkTime = 0f;
    public bool inarea;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.name == "GoblinGFX")
            {
                animator = child.gameObject.GetComponent<Animator>();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inarea)
        {
            if (Time.time >= nextAtkTime)
            {
                animator.SetBool("ATK" , true);                       
                nextAtkTime = Time.time + 1f / atkRate;
            }
            else
            {
                animator.SetBool("ATK", false);
            }
            transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else
        {
            transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.name != null)
        {
            if (col.tag == "Player")
            {
                inarea = true;
            }
        }
    }
    private void OnTriggerExit2D()
    {
            inarea = false;
    }
}
