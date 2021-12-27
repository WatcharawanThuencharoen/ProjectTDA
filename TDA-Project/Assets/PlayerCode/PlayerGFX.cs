using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGFX : MonoBehaviour
{

    public Animator animator;
    public Vector2 movement;
    public Vector2 facing;
    public Rigidbody2D rb;
    private string word;
    public ShowName setname;
    private float animlength;
    // Start is called before the first frame update
    void Start()
    {
        word = this.transform.parent.gameObject.name;
        rb = this.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        setname.setName(word);
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        getInput();
        if(Time.timeScale > 0)
        {
            animator.SetFloat("Horizontal", facing.x);
            animator.SetFloat("Vertical", facing.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        
    }

    private void FixedUpdate()
    {
        if (animlength > 0)
        {
            animlength -= Time.deltaTime;
        }
    }

    void getInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;
        if (Input.GetButtonDown("Fire1") && animlength <= 0)    
        {
            animlength = 0.4f;
        }
        if (movement != Vector2.zero)
        {
            if (animlength <= 0)
            {
                facing = movement;
            }
            
        }
    }
}
