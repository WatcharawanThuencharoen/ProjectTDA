using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private AudioClip arrowhit;
    AudioSource src;
    private int speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        arrowhit = Resources.Load<AudioClip>("arrowHit");
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.forward * 20;
    }

    private void FixedUpdate()
    {
        if (transform.eulerAngles.z == 0) //right
        {
            float factor = Time.deltaTime * speed;
            this.transform.Translate(factor, 0, 0, Space.World);
        }
        else if (transform.eulerAngles.z == 90) // up
        {
            float factor = Time.deltaTime * speed;
            this.transform.Translate(0, factor, 0, Space.World);
        }
        else if (transform.eulerAngles.z == 180) // left
        {
            float factor = Time.deltaTime * speed;
            this.transform.Translate(factor*-1, 0, 0, Space.World);
        }
        else if (transform.eulerAngles.z == 270) //down
        {
            float factor = Time.deltaTime * speed;
            this.transform.Translate(0, factor*-1, 0, Space.World);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit " +collision.name);
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "Goblin" || collision.gameObject.tag == "Slime" || collision.gameObject.tag == "Minotaur")
            {
                src.PlayOneShot(arrowhit);
                Debug.Log("Player hit " + collision.name);
                collision.GetComponent<HP>().TakeDamage(PlayerMovement.atk+PlayerMovement.weaponatk);
                Destroy(this.gameObject);
            }
        }
        if (collision.tag == "Obstacle" || collision.tag == "DestroyableObstacle")
        {
            src.PlayOneShot(arrowhit);
            Destroy(this.gameObject);
        }
    }
}
