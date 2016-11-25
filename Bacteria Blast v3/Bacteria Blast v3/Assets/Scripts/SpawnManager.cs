using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {

    public GameObject player;
    //public GameObject 
    public PlayerController playerStats;
    public int playerLives;
    public bool lifeLost = false;

    public float spawnTime = 5.0f;
    public Transform[] spawnPoints;
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        //Gets player's current health from PlayerController script
        
        player = GameObject.Find("Player");
        playerStats = player.gameObject.GetComponent<PlayerController>();
        playerLives = 3;
        

        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);

    }
	
	// Update is called once per frame
	void Update () {
        // If the player has no health left...       
         
         if (playerStats.currentHealth <= 2 && !playerStats.isDead)
         {
            GameObject icon = GameObject.Find("Blood Cell Health 3");
            icon.gameObject.GetComponent<Renderer>().enabled = false;
            if (playerStats.currentHealth <= 1 && !playerStats.isDead)
            {
                icon = GameObject.Find("Blood Cell Health 2");
                icon.gameObject.GetComponent<Renderer>().enabled = false;
                if (playerStats.currentHealth <= 0 && !playerStats.isDead)
                {
                    icon = GameObject.Find("Blood Cell Health 1");
                    icon.gameObject.GetComponent<Renderer>().enabled = false;
                    lifeLost = true;
                    if (playerStats.isDead == false && lifeLost == true)
                    {
                        playerLives--;
                        Debug.Log("Life Lost");

                        if (playerLives <= 2)
                        {
                            icon = GameObject.Find("Blood Cell Life 3");
                            icon.gameObject.GetComponent<Renderer>().enabled = false;
                            if (playerLives <= 1)
                            {
                                icon = GameObject.Find("Blood Cell Life 2");
                                icon.gameObject.GetComponent<Renderer>().enabled = false;
                                if (playerLives <= 0)
                                {
                                    icon = GameObject.Find("Blood Cell Life 1");
                                    icon.gameObject.GetComponent<Renderer>().enabled = false;
                                    SceneManager.LoadScene("Death Screen");
                                }
                                else
                                {
                                    icon = GameObject.Find("Blood Cell Health 1");
                                    icon.gameObject.GetComponent<Renderer>().enabled = true;
                                    icon = GameObject.Find("Blood Cell Health 2");
                                    icon.gameObject.GetComponent<Renderer>().enabled = true;
                                    icon = GameObject.Find("Blood Cell Health 3");
                                    icon.gameObject.GetComponent<Renderer>().enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                icon = GameObject.Find("Blood Cell Health 1");
                                icon.gameObject.GetComponent<Renderer>().enabled = true;
                                icon = GameObject.Find("Blood Cell Health 2");
                                icon.gameObject.GetComponent<Renderer>().enabled = true;
                                icon = GameObject.Find("Blood Cell Health 3");
                                icon.gameObject.GetComponent<Renderer>().enabled = true;
                                return;
                            }
                        }
                    }
                }
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

}
