using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public float atkRate = 2f;
    public float nextAtkTime = 0f;
    private float animlength;
    private int shotcount = 0;
    private Animator animator;
    public GameObject arrow;
    private Vector2 movement;
    private Quaternion facing;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        if (movement != Vector2.zero && animlength <= 0)
        {
            if (moveX > 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                transform.position = transform.parent.gameObject.transform.position + new Vector3(4.5f, 0.2f);
                facing = Quaternion.Euler(0, 0, 0);
            }
            else if (moveX < 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                transform.position = transform.parent.gameObject.transform.position + new Vector3(-4.5f, 0.2f);
                facing = Quaternion.Euler(0, 0, 180);
            }
            else if (moveY > 0 && Mathf.Abs(moveX) < Mathf.Abs(moveY))
            {
                transform.position = transform.parent.gameObject.transform.position + new Vector3(0f, 4.5f);
                facing = Quaternion.Euler(0, 0, 90);
            }
            else if (moveY < 0 && Mathf.Abs(moveX) < Mathf.Abs(moveY))
            {
                transform.position = transform.parent.gameObject.transform.position + new Vector3(0f, -4.5f);
                facing = Quaternion.Euler(0, 0, 270);
            }
            //facing = movement * 7.5f;
        }
        if (Time.time >= nextAtkTime)
        {
            if (Input.GetButtonDown("Fire1")) //mean using Spacebar - Check on Unity->Edit->Project Setting->Input     
            {
                animlength = animator.GetCurrentAnimatorStateInfo(0).length;
                nextAtkTime = Time.time + 1f / atkRate;
                shotcount = 1;
                
            }
        }
        if (animlength > 0)
        {
            animlength -= Time.deltaTime;
            if (animlength <= 0 && shotcount >0)
            {
                attack();
                shotcount--;
            }
        }
    }
    void attack()
    {
        GameObject Arrow = Instantiate(arrow, transform.position, facing);
        //Debug.Log(Arrow.transform.position);
        //Arrow. = transform.forward * 20;
 
    }
}
