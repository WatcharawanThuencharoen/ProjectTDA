using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class SlimeAI : MonoBehaviour
{
    private GameObject ObjectTarget;
    private Transform target;

    public Animator animator;
    public LayerMask enemyLayers;
    private Vector2 locate;
    private float timer,standby;
    private bool movecheck =false;
    private string[] randomColor = { "SlimeOrangeGFX", "SlimeBlueGFX", "SlimePurpleGFX" };
    private string rand;
    private int atkCount = 0;

    private string word;
    public ShowName setname;

    public static int atk = 5;
    public static int hp = 50;
    public static int def = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (StageCount.count >= 2)
        {
            //updateStat();
        }
        
        rand = randomColor[Random.Range(0, 3)];


        foreach (Transform child in transform)
        {
            //if (child.name == "SlimeOrangeGFX" && child.gameObject.activeSelf == true)
            //{
            //    animator = child.gameObject.GetComponent<Animator>();
            //}
            //else if (child.name == "SlimeBlueGFX" && child.gameObject.activeSelf == true)
            //{
            //    animator = child.gameObject.GetComponent<Animator>();
            //}
            //else if (child.name == "SlimePurpleGFX" && child.gameObject.activeSelf == true)
            //{
            //    animator = child.gameObject.GetComponent<Animator>();
            //}
            if (child.name == rand)
            {
                child.gameObject.SetActive(true);
                animator = child.gameObject.GetComponent<Animator>();
            }
        }
        ObjectTarget = GameObject.FindWithTag("Player");
        target = ObjectTarget.transform;
        //this.transform.gameObject.name = "Slime";
        //word = this.transform.gameObject.name;
        setname.setName(this.transform.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(movecheck) //if true || Slime move = Attack
        {
            if (!SlimeSound.src.isPlaying)
            {
                SlimeSound.playSound("attack");
            }
            attack(); // move
            timer -= Time.deltaTime; // until time up
            standby = 0; // then rest
            if (timer <= 0)
            {
                movecheck = false; // tell not move
                animator.SetBool("ATK", movecheck); // mean not attack
            }
        }
        else if(!movecheck) // if false || when stop moving
        {
            //SlimeSound.stopSound();
            standby += Time.deltaTime; // Wait for Move
            if (standby >= 1.0f) // after 1 sec
            {
                animator.SetBool("Ready", true); // to ready
            }
            if (standby >= 1.5f) // the after half sec
            {
                timer = 0.5f; // set time for move
                focus(); // set where to go
                movecheck = true; // tell slime is moving
                atkCount = 0; 
                animator.SetBool("ATK", movecheck); // mean slime attacking too
                animator.SetBool("Ready", false); // now it moving not ready
            }
            animator.SetFloat("Y", target.position.y - transform.position.y);
        }
        if(animator.GetBool("Dead"))
        {
            movecheck = false;
            animator.SetBool("ATK", false);
            animator.SetBool("Ready", false);
        }
    }

    private void focus()
    {
        locate = target.transform.position - this.transform.position;
        locate.Normalize();
        animator.SetFloat("X", target.position.x - transform.position.x);
        animator.SetFloat("Y", target.position.y - transform.position.y);
    }
    private void attack()
    {
        float factor = Time.deltaTime * Random.Range(50f, 120f);
        this.transform.Translate(locate.x * factor, locate.y * factor, 0, Space.World);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (movecheck)
        {
            if (col.tag == "Player" && atkCount < 1)
            {
                col.GetComponent<HP>().TakeDamage(atk);
                atkCount++;
            }
        }
    }
    private void updateStat()
    {
        if (StatCatch.slimeatk % 1 >= 0.5)
        {
            atk = (int)Mathf.Ceil(StatCatch.slimeatk);
        }
        else if (StatCatch.slimeatk % 1 < 0.5)
        {
            atk = (int)Mathf.Floor(StatCatch.slimeatk);
        }
        if (StatCatch.slimedef % 1 >= 0.5)
        {
            def = (int)Mathf.Ceil(StatCatch.slimedef);
        }
        else if (StatCatch.slimedef % 1 < 0.5)
        {
            def = (int)Mathf.Floor(StatCatch.slimedef);
        }
        if (StatCatch.slimehp % 1 >= 0.5)
        {
            hp = (int)Mathf.Ceil(StatCatch.slimehp);
        }
        else if (StatCatch.slimehp % 1 < 0.5)
        {
            hp = (int)Mathf.Floor(StatCatch.slimehp);
        }
    }

}
