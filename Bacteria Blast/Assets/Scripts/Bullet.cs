using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public int damage = 5;


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * Time.deltaTime * speed;

        float positionX = transform.position.x;
        float positionY = transform.position.y;
        
        //destroys the projectile object once it goes out of range of the game - must be done to not stack game objects and consume memory
        if (positionX < -14 || positionX > 14 || positionY < -12 || positionY > 12)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
