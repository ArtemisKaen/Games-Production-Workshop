using UnityEngine;
using System.Collections;

public class Bacteria : MonoBehaviour {

    public int bacteriaHealth = 10;
    public int bacteriaDamage = 1;
    public GameObject bullet;
    public Bullet bulletScript;

	// Use this for initialization
	void Start ()
    {
        //sets variable to hold bullet damage from projectile
        bulletScript = bullet.gameObject.GetComponent<Bullet>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (bacteriaHealth == 0)
        {
            kill();
        }
	}

    //add stuff like score added for player after death of enemy here
    void kill()
    {
        Destroy(gameObject);
    }

    //Makes checks for collision, then applies damage from Bullet script
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("contact");
        if (other.tag == "Projectile")
        {
            bacteriaHealth = bacteriaHealth - bulletScript.damage;
            Debug.Log ("Hit dat mofucka");
        }
        
    }


}
