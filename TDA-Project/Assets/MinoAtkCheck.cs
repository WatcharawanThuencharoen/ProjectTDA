using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoAtkCheck : MonoBehaviour
{
    private Animator animator;
    public float atkRate = 0.3f;
    public float nextAtkTime = 0f;
    public float timeforatk;
    public float atktimer = 0, atklen1 = 0, atklen2 = 0;
    public bool inarea;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.name == "MinotaurGFX")
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
            if(!animator.GetBool("ATK") && !animator.GetBool("ConATK"))
            {
                animator.SetBool("Ready", true);
            }
            //timeforatk = Time.time;
            if (Time.time >= nextAtkTime && animator.GetBool("Ready"))
            {
                animator.SetBool("ATK", true);
                animator.SetBool("Ready", false);
                atktimer = 0.5f;
                atklen1 = 1f;
                nextAtkTime = Time.time + 1f / atkRate;
            }
            else
            {
                if((animator.GetBool("ATK") || animator.GetBool("ConATK")) && !animator.GetBool("Ready") )
                {
                    atklen1 -= Time.deltaTime;
                    atklen2 -= Time.deltaTime;
                }
                if (atklen1 <= 0 && animator.GetBool("ATK"))
                {
                    
                    float con = Random.Range(0, 20);
                    if (con > 5)
                    {
                        animator.SetBool("ConATK", true);
                        atklen2 = 1.5f;
                        nextAtkTime = Time.time + 1f / atkRate;
                    }
                    animator.SetBool("ATK", false);
                    //nextAtkTime = Time.time + 1f / atkRate;
                }
                else if (atklen2 <= 0 && animator.GetBool("ConATK"))
                {
                    animator.SetBool("ConATK", false);
                }
                //animator.SetBool("ATK", false);
                //animator.SetBool("ConATK", false);
            }
        }
        else
        {
            animator.SetBool("ATK", false);
            animator.SetBool("ConATK", false);
            animator.SetBool("Ready", false);
            //atktimer = 0.5f;
            //atklen1 = 0.3f;
            //atklen2 = 1.0f;
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
