using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    GameObject Player;

    public int startHealth = 10;
    public int currentHealth;
    public float moveSpeed = 0.5f;
    public Vector2 playerPosition;
    public int bacteria;
    public Bacteria bacteriaScript;
    //bool isDead;
    //bool damaged;


    // Use this for initialization
    void Start()
    {
        //health stats possibly working, can't check while colliders aren't functioning.
        currentHealth = startHealth;
        GameObject enemyDamage = GameObject.Find("Bacteria 1");
        bacteriaScript = enemyDamage.GetComponent<Bacteria>();
    }

    // Update is called once per frame
    void Update()
    {
        //float f = 1.1f;
        //int i = (int)f;
        float xPos = gameObject.transform.position.x + (Input.GetAxis("Horizontal") * moveSpeed);
        float yPos = gameObject.transform.position.y + (Input.GetAxis("Vertical") * moveSpeed);
        // Mathf.Clamp sets the range of movement for the player
        playerPosition = new Vector2(Mathf.Clamp(xPos, -14, 14), Mathf.Clamp(yPos, -12, 12));
        gameObject.transform.position = playerPosition;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mouseWorldPosition.z = 0.0f;

            GameObject bullet = (GameObject)Instantiate(Resources.Load("Projectile"));
            bullet.transform.position = transform.position;

            Bullet bulletClass = bullet.GetComponent<Bullet>();
            Vector3 direction = mouseWorldPosition - transform.position;
            direction.Normalize();
            bulletClass.direction = direction;


        }
    }

    //Below is more testing work for the health and death of the player, mostly from a template


    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("contact");
        if (other.tag == "Enemy")
        {
            currentHealth = currentHealth - bacteriaScript.bacteriaDamage;

            Debug.Log("Hit dat mofucka");
        }
    }
    

    /*
    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
    }
    */
}