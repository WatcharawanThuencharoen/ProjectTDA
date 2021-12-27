using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1,enemy2;
    float randX;
    float randY;
    Vector2 wheretospawn;
    private float spawnrate = 2;
    private int countpershowup = 4;
    [HideInInspector] public int i = 1;
    private float nextSpawn = 0.0f;
    public static EnemySpawner instance;

    // Start is called before the first frame update
    private void Start()
    {
        countpershowup += StageCount.count;
    }
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawn();
    }

        
    void spawn()    
    {
        if (Time.time > nextSpawn)
        {
            if (i <= countpershowup)
            {

                i++;
                nextSpawn = Time.time + spawnrate;
                //randX = Random.Range(transform.position.x - 1f, transform.position.x + 1f); //400
                //randY = Random.Range(transform.position.y - 1f, transform.position.y + 1f); //225
                randX = Random.Range(-335, 335); //335
                randY = Random.Range(-185, 185); //185
                wheretospawn = new Vector2(randX, randY);
                GameObject Spawn;
                if (StageCount.count <= 1 && SceneManager.GetActiveScene().name == "Forest")
                {
                    Spawn = Instantiate(enemy1, wheretospawn, Quaternion.identity);
                    Spawn.name = "Goblin";
                }
                else if (StageCount.count <= 1 && SceneManager.GetActiveScene().name == "Plain")
                {
                    Spawn = Instantiate(enemy2, wheretospawn, Quaternion.identity);
                    Spawn.name = "Slime";
                }
                else
                {
                    if (Random.Range(0, 2) <= 0)
                    {
                        Spawn = Instantiate(enemy1, wheretospawn, Quaternion.identity);
                        Spawn.name = "Goblin";
                    }
                    else
                    {
                        Spawn = Instantiate(enemy2, wheretospawn, Quaternion.identity);
                        Spawn.name = "Slime";
                    }
                }

            }
        }
    }
}
