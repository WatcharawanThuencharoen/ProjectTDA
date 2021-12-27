using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    
    private TextMesh text;
    [SerializeField] private Transform DMPopUp;
    public GameObject item;
    private string[] allitem = { "AtkMedal", "DefMedal", "HpMedal", "Sword", "AtkMedal", "DefMedal", "HpMedal", "Spear", "AtkMedal", "DefMedal", "HpMedal", "Bow", "AtkMedal", "DefMedal", "HpMedal"};
    private Vector3 popupposition;
    private int maxHP;
    private int def;
    int CurrentHP;
    
    public HPBar hpBar;

    void Start()
    {
        getStatus();        
        setHP();

        //TakeDamage(50);
    }

    private void FixedUpdate()
    {
        getStatus();
        updateHP();
    }

    private void getStatus()
    {
        if(transform.tag == "Goblin")
        {
            maxHP = GoblinAI.hp;
            def = GoblinAI.def;
        }
        else if (transform.tag == "Slime")
        {
            maxHP = SlimeAI.hp;
            def = SlimeAI.def;
        }
        else if (transform.tag == "Minotaur")
        {
            maxHP = MinotaurAI.hp;
            def = MinotaurAI.def;
        }
        else if (transform.tag == "Player")
        {
            maxHP = PlayerMovement.hp;
            def = PlayerMovement.def;
        }
    }

    private void setHP()
    {
        CurrentHP = maxHP;
        hpBar.setMaxHealth(maxHP);
        hpBar.setHealth(maxHP);
    }

    public void updateHP()
    {
        hpBar.setMaxHealth(maxHP);
    }


    public void TakeDamage(int atk)
    {
        int damage = atk-def;
        if(damage < 1)
        {
            damage = 1;
        }
        if(CurrentHP > 0)
        {
            popupposition = transform.position;
            popupposition += new Vector3(Random.Range(-5f, 5f), 15f, 0);
            DamagePopUpHandler.damage = damage;
            Instantiate(DMPopUp, popupposition, Quaternion.identity);
            CurrentHP -= damage;
            Debug.Log(transform.name + "HP " + CurrentHP + "/" + maxHP);
            hpBar.setHealth(CurrentHP);
            if (CurrentHP <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log(this.name + " Die!!");
        
        if(this.tag == "Player")
        {
            PlayerSound.playSound("dead");
            PlayerMovement.animator.SetBool("Dead", true);
            //SceneManager.LoadScene("MainMenu");
        }
        if (this.tag == "Goblin")
        {
            GoblinSound.playSound("dead");
            EnemySpawner.instance.i--;
            StatCatch.goblinkill++;
            StatCatch.onegamekill++;
            spawnItem();
            Animator anim = gameObject.transform.Find("GoblinGFX").GetComponent<Animator>();
            anim.SetBool("Dead", true);
            //Destroy(gameObject);
        }
        if (this.tag == "Slime")
        {
            SlimeSound.playSound("dead");
            EnemySpawner.instance.i--;
            StatCatch.slimekill++;
            StatCatch.onegamekill++;
            spawnItem();
            Animator anim = transform.Find("SlimeOrangeGFX").GetComponent<Animator>();
            foreach (Transform child in transform)
            {
                
                if (child.name == "SlimeOrangeGFX" && child.gameObject.activeSelf == true)
                {
                    anim = child.GetComponent<Animator>();
                }
                else if (child.name == "SlimeBlueGFX" && child.gameObject.activeSelf == true)
                {
                    anim = child.GetComponent<Animator>();
                }
                else if (child.name == "SlimePurpleGFX" && child.gameObject.activeSelf == true)
                {
                    anim = child.GetComponent<Animator>();
                }
            }
            anim.SetBool("Dead", true);
            //Destroy(gameObject);
        }
        if (this.tag == "Minotaur")
        {
            MinoSound.playSound("dead");
            Animator anim = gameObject.transform.Find("MinotaurGFX").GetComponent<Animator>();
            StatCatch.onegamekill++;
            anim.SetBool("Dead", true);
            //Destroy(gameObject);
        }

    }

    void spawnItem()
    {
        Debug.Log("TrytoSpawnItem");
        if (Random.Range(1,101)%2 == 0)
        {
            GameObject Spawn;
            Spawn = Instantiate(item, this.transform.position, Quaternion.identity);
            Spawn.name = allitem[Random.Range(0, allitem.Length)];
        }
        
    }

}
