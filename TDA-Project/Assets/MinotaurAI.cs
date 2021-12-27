using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAI : MonoBehaviour
{
    private GameObject ObjectTarget;
    private Transform target;

    public static Animator animator;
    public LayerMask enemyLayers, obtacleLayers;
    private Vector2 locate;
    private float timer, standby, lenATK1, lenATK2;
    private bool movecheck = false;
    public static bool atkcheck1 = false, atkcheck2 = false;
    private bool rleg = false;
    private int hitcount = 0;
    private int maxhit = 1;
    private Vector2 facing;
    private Vector2 atkzone;

    private string word;
    public ShowName setname;

    public static int atk = 20;
    public static int hp = 500;
    public static int def = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (StageCount.count >= 2)
        {
            //updateStat();
        }
        foreach (Transform child in transform)
        {
            if (child.name == "MinotaurGFX")
            {
                animator = child.gameObject.GetComponent<Animator>();
            }
            ObjectTarget = GameObject.FindWithTag("Player");
            target = ObjectTarget.transform;
            this.transform.gameObject.name = "Minotaur";
            word = this.transform.gameObject.name;
            setname.setName(word);
        }
    }
    void FixedUpdate()
    {
        if (!animator.GetBool("Dead"))
        {
            if (animator.GetBool("ATK") || animator.GetBool("ConATK") || animator.GetBool("Ready"))
            {
                MinoWalkSound.stopSound();
                movecheck = false;
                animator.SetBool("Move", movecheck);
            }
            
            if (movecheck && !(animator.GetBool("ATK") || animator.GetBool("ConATK") || animator.GetBool("Ready")))
            {
                if (!MinoWalkSound.src.isPlaying)
                {
                    MinoWalkSound.playSound();
                }
                animator.SetBool("Move", movecheck);
                walk();
                timer -= Time.deltaTime;
                standby = 0;
                if (timer <= 0)
                {
                    
                    movecheck = false;
                    rleg = !rleg;
                    animator.SetBool("RightLeg", rleg);
                }
            }
            else if (!movecheck && !(animator.GetBool("ATK") || animator.GetBool("ConATK") || animator.GetBool("Ready")))
            {
                
                standby += Time.deltaTime;
                lenATK1 = 0.3f;
                lenATK2 = 1f;
                if (standby >= 1.0f)
                {

                    timer = 0.2f; // set time for move
                    focus(); // set where to go
                    movecheck = true; // tell is moving
                }
                animator.SetFloat("Horizontal", target.position.x - transform.position.x);
                animator.SetFloat("Vertical", target.position.y - transform.position.y);
            }


            //if (animator.GetBool("ATK"))
            //{
            //    lenATK1 = 0.3f;
            //    atkcheck1 = true;
            //}
            //if (animator.GetBool("ConATK"))
            //{
            //    lenATK2 = 1f;
            //    atkcheck2 = true;
            //}
            //Debug.Log(lenATK);
            //lenATK1 -= Time.deltaTime;
            //lenATK2 -= Time.deltaTime;
            //if (lenATK1 <= 0 && atkcheck1)
            //{
            //    atkDmg();

            //}
            //else if (lenATK2 <= 0 && atkcheck2)
            //{
            //    conAtkDmg();
            //}
            if (atkcheck1)
            {
                atkDmg();
                MinoSound.playSound("attack1");

            }
            else if (atkcheck2)
            {
                conAtkDmg();
                MinoSound.playSound("attack2");
            }
        }
        
        if (animator.GetBool("Dead"))
        {
            movecheck = false;
            animator.SetBool("ATK", false);
            animator.SetBool("ConATK", false);
            animator.SetBool("Ready", false);
        }
    }

    private void atkDmg()
    {
        hitcount = 0;
        maxhit = 1;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll((Vector2)transform.position + facing, atkzone, enemyLayers);
        if (hitEnemies != null)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                //Debug.Log(enemy + " " + atk);
                if (enemy.tag == "Player" && enemy.isTrigger && hitcount < maxhit)
                {
                    Debug.Log(enemy + " Component Trigger " + atk);
                    enemy.GetComponent<HP>().TakeDamage(atk);
                    hitcount++;
                }
            }
        }
        atkcheck1 = false;
    }
    private void conAtkDmg()
    {
        maxhit = 2;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll((Vector2)transform.position + facing, new Vector2(27.5f, 27.5f), enemyLayers);
        Collider2D[] hitObtacle = Physics2D.OverlapBoxAll((Vector2)transform.position + facing, new Vector2(27.5f, 27.5f), obtacleLayers);
        if (hitEnemies != null)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                //Debug.Log(enemy + " " + atk);
                if (enemy.tag == "Player" && enemy.isTrigger && hitcount < maxhit)
                {
                    Debug.Log(enemy + " Component Trigger " + atk);
                    enemy.GetComponent<HP>().TakeDamage(atk);
                    hitcount++;
                }
            }
            foreach (Collider2D Obs in hitObtacle)
            {
                if (Obs.tag == "Obstacle")
                {
                    Debug.Log("Destroy");
                    Destroy(Obs.gameObject);
                }
            }
        }
        atkcheck2 = false;
    }

    private void focus()
    {
        locate = target.transform.position - this.transform.position;
        locate.Normalize();
        float hor = locate.x;
        float ver = locate.y;
        if (hor > 0 && Mathf.Abs(hor) > Mathf.Abs(ver))
        {
            facing = new Vector2(14f, 5f);
            atkzone = new Vector2(27.5f, 55f);
        }
        else if (hor < 0 && Mathf.Abs(hor) > Mathf.Abs(ver))
        {
            facing = new Vector2(-14f, 5f);
            atkzone = new Vector2(27.5f, 55f);
        }
        else if (ver > 0 && Mathf.Abs(hor) < Mathf.Abs(ver))
        {
            facing = new Vector2(0f, 19f);
            atkzone = new Vector2(55f, 27.5f);
        }
        else if (ver < 0 && Mathf.Abs(hor) < Mathf.Abs(ver))
        {
            facing = new Vector2(0f, -8.5f);
            atkzone = new Vector2(55f, 27.5f);
        }
        //animator.SetFloat("X", target.position.x - transform.position.x);
        //animator.SetFloat("Y", target.position.y - transform.position.y);
    }
    private void walk()
    {
        float factor = Time.deltaTime * 100;
        this.transform.Translate(locate.x * factor, locate.y * factor, 0, Space.World);
    }

    private void updateStat()
    {
        if (StatCatch.minoatk % 1 >= 0.5)
        {
            atk = (int)Mathf.Ceil(StatCatch.minoatk);
        }
        else if (StatCatch.minoatk % 1 < 0.5)
        {
            atk = (int)Mathf.Floor(StatCatch.minoatk);
        }
        if (StatCatch.minodef % 1 >= 0.5)
        {
            def = (int)Mathf.Ceil(StatCatch.minodef);
        }
        else if (StatCatch.minodef % 1 < 0.5)
        {
            def = (int)Mathf.Floor(StatCatch.minodef);
        }
        if (StatCatch.minohp % 1 >= 0.5)
        {
            hp = (int)Mathf.Ceil(StatCatch.minohp);
        }
        else if (StatCatch.minohp % 1 < 0.5)
        {
            hp = (int)Mathf.Floor(StatCatch.minohp);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireCube(rb.position + new Vector2(14f, 5f), new Vector2(27.5f, 55f));
        Gizmos.DrawWireCube((Vector2)transform.position + facing, atkzone);
        Gizmos.DrawWireCube((Vector2)transform.position + facing, new Vector2(27.5f, 27.5f));
    }
}
