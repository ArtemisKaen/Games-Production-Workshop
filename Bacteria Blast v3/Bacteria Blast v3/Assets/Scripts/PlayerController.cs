using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    GameObject Player;

    public int startHealth = 5;
    public int currentHealth;
    public float moveSpeed = 0.5f;
    public Vector2 playerPosition;
    public int bacteria;

    public Collider2D playerCollider;
    public bool isDead;
    bool damagedEffect;
    bool damaged;

    // Use this for initialization
    void Start()
    {
        //health stats possibly working, can't check while colliders aren't functioning.
        currentHealth = startHealth;
        damaged = false;
        damagedEffect = false;
        playerCollider = GetComponent<Collider2D>();
        playerCollider.enabled = true;
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
        //while (!isDead)
        //{
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

            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die. 
                Debug.Log("should die");
                isDead = true;
                StartCoroutine(death());
            }
        //}
    }

    IEnumerator drainHealth()
    {
        while (damagedEffect == true)
        {
            Debug.Log("blood cell damage");
            damaged = true;
            currentHealth--;
            if (currentHealth <=0)
            {
                yield break;
            }
            StartCoroutine(hitBoxTrigger());
            yield return new WaitForSeconds(1.5f);
            damaged = false;
        }

        damagedEffect = true;
    }

    IEnumerator death()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        playerCollider.enabled = !playerCollider.enabled;
        Debug.Log("blood cell death");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        StartCoroutine(hitBoxTrigger());
        currentHealth = startHealth;
        transform.position = Vector2.zero;
        gameObject.GetComponent<Renderer>().enabled = true;
        damaged = false;
        damagedEffect = false;
        isDead = false;
        yield return new WaitForSeconds(3.0f);
        playerCollider.enabled = !playerCollider.enabled;
    }

    IEnumerator hitBoxTrigger()
    {
        // Flashes renderer on and off 5 times in a second to emulate a flashing icon.
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        while (!damaged)
        {
            if (other.tag == "Enemy")
            {
                StartCoroutine(drainHealth());
                Debug.Log("Hit taken");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            damagedEffect = false;
            Debug.Log("stop");
        }
    }

}