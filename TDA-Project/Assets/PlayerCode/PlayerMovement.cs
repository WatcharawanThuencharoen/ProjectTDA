using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public static Animator animator;
    private Transform itemCheckSpace;
    


    public static int atk = 0;
    public static int hp = 20;
    public static int def = 0;
    public static int weaponatk;
    public static string curweapon;
    private int damage = 0;
    public static Vector2 lastLocation;

    public Vector2 movement;
    public Vector2 facing;


    private float dashSpeed = 4;
    private float dashTime = 0;
    private float startDashTime = 2f;
    private bool isjump;
    private Collider2D[] hitEnemies;


    private float atkRate = 2f;
    private float nextAtkTime = 0f;
    private float AtkRange = 5;
    private float animlength;
    public LayerMask enemyLayers;

    // Update is called once per frame

    private void Start()
    {
        transform.name = StatCatch.playerProfile;
        dashTime = startDashTime;
        StatCatch.setPlayerStat();
        updateAnime();
        //transform.position = new Vector2(360, 180);
    }

    void Update() // Up to FPS
    {
        if (!animator.GetBool("Dead") || !StatCatch.inMenu)
        {
            getInput();
            if (Time.time >= nextAtkTime)
            {
                if (Input.GetButtonDown("Fire1")) //mean using Spacebar - Check on Unity->Edit->Project Setting->Input     
                {
                    attack();
                    nextAtkTime = Time.time + 1f / atkRate;
                    if (animlength > 0)
                    {
                        animlength -= Time.deltaTime;
                    }
                }
            }
        }
    }

    private void FixedUpdate() //Up to Time
    {
        itemCheckSpace.gameObject.transform.position = rb.position + facing;
        lastLocation = transform.position;
        
        walk();
        dash();

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
        
        if (movement != Vector2.zero)
        {
            if (moveX > 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                facing = new Vector2(7.5f, 0f);
            }
            else if (moveX < 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                facing = new Vector2(-7.5f, 0f);
            }
            else if (moveY > 0 && Mathf.Abs(moveX) < Mathf.Abs(moveY))
            {
                facing = new Vector2(0f, 7.5f);
            }
            else if (moveY < 0 && Mathf.Abs(moveX) < Mathf.Abs(moveY))
            {
                facing = new Vector2(0f, -7.5f);
            }
            //facing = movement * 7.5f;
        }
    }

    void walk()
    {
        if (animlength > 0) 
        {
            rb.velocity = Vector2.zero;
            Walksound.stopSound();
        }
        else
        {
            rb.velocity = new Vector2(movement.x * moveSpeed , movement.y * moveSpeed);
            if (!Walksound.src.isPlaying)
            {
                Walksound.playSound();
            }  
            if (rb.velocity == new Vector2(0, 0))
            {
                Walksound.stopSound();
            }
        }
    }
    void dash()
    {
        if (isjump == false)
        {
            if (Input.GetButtonDown("Jump")) //mean using LeftShift - Check on Unity->Edit->Project Setting->Input
            {
                isjump = true;
                
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                isjump = false;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                dashTime -= Time.deltaTime;
                GetComponent<BoxCollider2D>().enabled = false;
                rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed) * dashSpeed;
                
            }
        }
    }
    

    void attack()
    {
        //updateAnime();
        damage = atk + weaponatk;
        
        animator.SetTrigger("Attack");
        if(curweapon == "Sword")
        {
            PlayerSound.playSound("attack");
            animlength = animator.GetCurrentAnimatorStateInfo(0).length;
            if (facing.x != 0)
            {
                hitEnemies = Physics2D.OverlapBoxAll(rb.position + facing , new Vector2(AtkRange * 4, 3.5f), enemyLayers);
            }
            else
            {
                hitEnemies = Physics2D.OverlapBoxAll(rb.position + facing + new Vector2(0, 2), new Vector2(3.5f, AtkRange * 4), enemyLayers);
            }
        }
        else if (curweapon == "Spear")
        {
            PlayerSound.playSound("attack");
            animlength = animator.GetCurrentAnimatorStateInfo(0).length;
            if (facing.y != 0)
            {
                hitEnemies = Physics2D.OverlapBoxAll(rb.position + facing + new Vector2(0, 2), new Vector2(AtkRange * 4, 3.5f), enemyLayers);
            }
            else
            {
                hitEnemies = Physics2D.OverlapBoxAll(rb.position + facing + new Vector2(0, 2), new Vector2(3.5f, AtkRange * 4), enemyLayers);
            }
        }
        else if (curweapon == "Bow")
        {
            PlayerSound.playSound("attackBow");
            animlength = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        //Damage
        if (hitEnemies != null)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.isTrigger)
                {
                    if (enemy.gameObject.tag == "Goblin" || enemy.gameObject.tag == "Slime" || enemy.gameObject.tag == "Minotaur" || enemy.gameObject.tag == "Dummy")
                    {
                        Debug.Log("Player hit " + enemy.name);
                        enemy.GetComponent<HP>().TakeDamage(damage);
                    }
                }
            }
            hitEnemies = null;
        }
    }


    public void updateAnime()
    {
        foreach (Transform child in transform)
        {
            if (child.name == (curweapon+"GFX"))
            {
                child.gameObject.SetActive(true);
                animator = child.gameObject.GetComponent<Animator>();
            }
            if (child.name == "ItemCheck")
            {
                itemCheckSpace = child;
            }
        }
    }

    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(rb.position+facing, AtkRange);
        if(facing.x != 0)
        {
            //Gizmos.DrawWireCube(rb.position+facing+new Vector2(0,2), new Vector2(AtkRange*4,3.5f));
            Gizmos.DrawWireCube(rb.position + facing + new Vector2(0, 2), new Vector2(3.5f, AtkRange * 4));
        }
        else
        {
            Gizmos.DrawWireCube(rb.position + facing + new Vector2(0, 2), new Vector2(AtkRange * 4, 3.5f));
            
            //Gizmos.DrawWireCube(rb.position+facing + new Vector2(0, 2), new Vector2(3.5f, AtkRange*4));
        }
        
    }

}
