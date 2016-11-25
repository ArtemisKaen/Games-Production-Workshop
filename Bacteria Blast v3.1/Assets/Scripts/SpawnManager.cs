using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {

    public GameObject player;
    //public GameObject 
    public PlayerController playerStats;
    public int playerLives;
    public bool lifeLost = false;
    public GameObject[] healthIcons;
    public GameObject[] lifeIcons;

    public float spawnTime = 5.0f;
    public Transform[] spawnPoints;
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        //Gets player's current health from PlayerController script
        
        player = GameObject.Find("Player");
        playerStats = player.gameObject.GetComponent<PlayerController>();
        playerLives = 3;

        healthIcons = GameObject.FindGameObjectsWithTag("Health");
        for (int i = 0; i<healthIcons.Length; i++)
        {
            healthIcons[i] = GameObject.Find("Blood Cell Health " + (i+1));
        }
        lifeIcons = GameObject.FindGameObjectsWithTag("Lives");
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i] = GameObject.Find("Blood Cell Life " + (i+1));
        }

        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);

    }
	
	// Update is called once per frame
	void Update ()
    {
        healthIconManagement();
        if (playerStats.currentHealth <= 0 && !playerStats.isDead)
        {
            healthIcons[0].gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("lost health 1");
            lifeLost = true;
            if (playerStats.isDead == false && lifeLost == true)
            {
                playerLives--;
                playerStats.isDead = true;
                Debug.Log("Life Lost");
                //Adjust player lives.
                lifeIconManagement();
            }
        }
    }

    void SpawnEnemy()
    {
        // Find a random index between zero and one less than the number of spawn points.
        //"Length-1" might not be correct but it made sense to me
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemies.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    void healthIconManagement()
    {
        if (playerStats.currentHealth <= 2 && !playerStats.isDead)
        {
            healthIcons[2].gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("lost health 3");
            if (playerStats.currentHealth <= 1 && !playerStats.isDead)
            {
                healthIcons[1].gameObject.GetComponent<Renderer>().enabled = false;
                Debug.Log("lost health 2");
            }
        }
    }

    void lifeIconManagement()
    {
        if (playerLives <= 2)
        {
            lifeIcons[2].gameObject.GetComponent<Renderer>().enabled = false;
            if (playerLives <= 1)
            {
                lifeIcons[1].gameObject.GetComponent<Renderer>().enabled = false;
                if (playerLives <= 0)
                {
                    lifeIcons[0].gameObject.GetComponent<Renderer>().enabled = false;
                    SceneManager.LoadScene("Death Screen");
                }
                else
                {
                    healthIcons[0].gameObject.GetComponent<Renderer>().enabled = true;
                    healthIcons[1].gameObject.GetComponent<Renderer>().enabled = true;
                    healthIcons[2].gameObject.GetComponent<Renderer>().enabled = true;
                    return;
                }
            }
            else
            {
                healthIcons[0].gameObject.GetComponent<Renderer>().enabled = true;
                healthIcons[1].gameObject.GetComponent<Renderer>().enabled = true;
                healthIcons[2].gameObject.GetComponent<Renderer>().enabled = true;
                return;
            }
        }
    }



}
