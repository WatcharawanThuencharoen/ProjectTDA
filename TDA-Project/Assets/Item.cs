using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string word;
    public ShowName setname;
    public GameObject ShowName;
    private GameObject ObjectTarget;
    private string tempweapon;
    //private Transform target;

    // Start is called before the first frame update
    void Start()
    {   
        //this.transform.gameObject.name = "AtkMedal";
        //word = this.transform.gameObject.name;
        //setname.setName(word);
        ShowName.SetActive(false);

        ObjectTarget = GameObject.FindWithTag("Player");
        foreach (Transform child in transform)
        {
            if (child.name == transform.name)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowName.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerSound.playSound("CollectItem");
                Debug.Log("KEY Press");
                tempweapon = PlayerMovement.curweapon;
                if (transform.name == "Sword")
                {
                    foreach (Transform child in ObjectTarget.transform)
                    {
                        if (child.name == "SwordGFX")
                        {
                            child.gameObject.SetActive(true);
                            PlayerMovement.animator = child.gameObject.GetComponent<Animator>();
                            PlayerMovement.curweapon = "Sword";
                            PlayerMovement.weaponatk = 20;
                            swapItem();
                        }
                        else if (child.name == "SpearGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                        else if (child.name == "BowGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                else if (transform.name == "Spear")
                {
                    foreach (Transform child in ObjectTarget.transform)
                    {
                        if (child.name == "SwordGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                        else if (child.name == "SpearGFX")
                        {
                            child.gameObject.SetActive(true);
                            PlayerMovement.animator = child.gameObject.GetComponent<Animator>();
                            PlayerMovement.curweapon = "Spear";
                            PlayerMovement.weaponatk = 15;
                            swapItem();
                        }
                        else if (child.name == "BowGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                else if (transform.name == "Bow")
                {
                    foreach (Transform child in ObjectTarget.transform)
                    {
                        if (child.name == "SwordGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                        else if (child.name == "SpearGFX")
                        {
                            child.gameObject.SetActive(false);
                        }
                        else if (child.name == "BowGFX")
                        {
                            child.gameObject.SetActive(true);
                            PlayerMovement.animator = child.gameObject.GetComponent<Animator>();
                            PlayerMovement.curweapon = "Bow";
                            PlayerMovement.weaponatk = 10;
                            swapItem();
                        }
                    }
                }
                else if (transform.name == "AtkMedal")
                {
                    PlayerMovement.atk += 1;
                    Destroy(gameObject);
                }
                else if (transform.name == "DefMedal")
                {
                    PlayerMovement.def += 1;
                    Destroy(gameObject);
                }
                else if (transform.name == "HpMedal")
                {
                    PlayerMovement.hp += 5;
                    Destroy(gameObject);
                }



                //HP.updateHP();
                //Destroy(gameObject);
            }
        }
    }

    private void swapItem()
    {
        transform.name = tempweapon;
        foreach (Transform child in transform)
        {
            if (child.name == transform.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerItemCheck")
        {
            setname.setName(transform.name);
            ShowName.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShowName.SetActive(false);
    }
}
