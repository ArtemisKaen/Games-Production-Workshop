using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

    public GameObject player;
    public PlayerController playerHealth;
    public float spawnTime = 5.0f;
    public Transform[] spawnPoints;
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        //Gets player's current health from PlayerController script
        playerHealth = player.gameObject.GetComponent<PlayerController>();
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);

    }
	
	// Update is called once per frame
	void Update () {

	}

    void SpawnEnemy()
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        //"Length-1" might not be correct but it made sense to me
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemies.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    /*
     * ignore this shit
     * 
    public IEnumerator Do()
    {
        yield return new WaitForSeconds(5);
        // code to be executed after 5 secs
    }

    
    public void Awake()
    {
        Instantiate(prefab);
        yield return StartCoroutine("Do");

    }
    */
}
