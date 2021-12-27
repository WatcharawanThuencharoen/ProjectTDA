using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class GoblinAI : MonoBehaviour
{
    private GameObject ObjectTarget;
    private Transform target;

    private float speed = 30f; //30 200
    private float nextWaypointDistance = 100f; //30 3

    Path path;
    int currentWaypoint = 0;
    bool reached = false;

    Seeker seeker;
    Rigidbody2D rb;
    public Animator animator;
    Vector2 direction;
    private float AtkRange = 4f; //4
    private float AtkTimer;
    Vector2 facing;
    public LayerMask enemyLayers;
    bool ac = false;
    
    public ShowName setname;

    public static int atk = 10;
    public static int hp = 75;
    public static int def = 0;
    private int hitcount = 0;



    // Start is called before the first frame update
    void Start()
    {
        if (StageCount.count >= 2)
        {
            //updateStat();
        }

        animator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5F);
    
        ObjectTarget = GameObject.FindWithTag("Player");
        target = ObjectTarget.transform;
        setname.setName(this.transform.gameObject.name);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reached = true;
            return;
        }
        else
        {
            reached = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.velocity = new Vector2(direction.x * speed  , direction.y * speed);

       
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (!animator.GetBool("Dead"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ATK"))
            {
                
                AtkTimer -= Time.deltaTime;
                if (AtkTimer <= 0)
                {

                    float hor = target.position.x - rb.position.x;
                    float ver = target.position.y - rb.position.y;
                    if (hor > 0 && Mathf.Abs(hor) > Mathf.Abs(ver))
                    {
                        facing = new Vector2(7.5f, 0f);
                    }
                    else if (hor < 0 && Mathf.Abs(hor) > Mathf.Abs(ver))
                    {
                        facing = new Vector2(-7.5f, 0f);
                    }
                    else if (ver > 0 && Mathf.Abs(hor) < Mathf.Abs(ver))
                    {
                        facing = new Vector2(0f, 7.5f);
                    }
                    else if (ver < 0 && Mathf.Abs(hor) < Mathf.Abs(ver))
                    {
                        facing = new Vector2(0f, -7.5f);
                    }
                }
            }
            if (!GoblinWalk.src.isPlaying)
            {
                GoblinWalk.playSound();
            }
            updatePositionAnim(target.position.x - rb.position.x, target.position.y - rb.position.y);
            updateAtkAnim();
        }

        

    }

    private void OnCollisionStay2D(Collision2D col)
    {

        if (col.collider.tag == "Player")
        {
            GoblinWalk.stopSound();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
            

    }
    private void OnCollisionExit2D()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void updatePositionAnim(float wayX, float wayY)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            if (wayX != 0)
                animator.SetFloat("Horizontal", wayX);

            if (wayY != 0)
                animator.SetFloat("Vertical", wayY);
        }
    }
    private void updateAtkAnim()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATK"))
        {
            GoblinWalk.stopSound();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            AtkTimer = 0.04f;
            ac = true;
            
        }
        else if (ac)
        {
            GoblinSound.playSound("attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(rb.position + facing, AtkRange, enemyLayers);
            if (hitEnemies != null)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log(enemy + " " + atk);
                    if (enemy.isTrigger && hitcount < 1)
                    {
                        Debug.Log(enemy + " Component Trigger " + atk);
                        enemy.GetComponent<HP>().TakeDamage(atk);
                        hitcount++;
                    }
                }
                ac = false;
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ac = false;
            hitcount = 0;
            
        }
    }
    //private void updateStat()
    //{
    //    if (StatCatch.goblinatk % 1 >= 0.5)
    //    {
    //        atk = (int)Mathf.Ceil(StatCatch.goblinatk);
    //    }
    //    else if (StatCatch.goblinatk % 1 < 0.5)
    //    {
    //        atk = (int)Mathf.Floor(StatCatch.goblinatk);
    //    }
    //    if (StatCatch.goblindef % 1 >= 0.5)
    //    {
    //        def = (int)Mathf.Ceil(StatCatch.goblindef);
    //    }
    //    else if (StatCatch.goblindef % 1 < 0.5)
    //    {
    //        def = (int)Mathf.Floor(StatCatch.goblindef);
    //    }
    //    if (StatCatch.goblinhp % 1 >= 0.5)
    //    {
    //        hp = (int)Mathf.Ceil(StatCatch.goblinhp);
    //    }
    //    else if (StatCatch.goblinhp % 1 < 0.5)
    //    {
    //        hp = (int)Mathf.Floor(StatCatch.goblinhp);
    //    }
    //}

    private void OnDrawGizmosSelected()
    {

        //Gizmos.DrawWireSphere(rb.position + facing, AtkRange);
    }
}
