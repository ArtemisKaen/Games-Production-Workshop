using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bacteria : MonoBehaviour {

    public int bacteriaHealth;
    public GameObject bullet;
    public Bullet bulletScript;
	public GameObject defenceObject;
	private DefencePoint defencePointScript;
	private Text healthText;

	private bool stopDrainHealth = false;

	// Use this for initialization
	void Start ()
    {
        bulletScript = bullet.gameObject.GetComponent<Bullet>();

		defenceObject = GameObject.Find("Defence Point");
		defencePointScript = defenceObject.GetComponent<DefencePoint>();

		GameObject healthTextGO = GameObject.Find("healthText");
		healthText = healthTextGO.GetComponent<Text>();

		UpdateHealth ();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (bacteriaHealth <= 0)
        {
            kill();
        }

	}

    IEnumerator drainHealth()
    {
        while (stopDrainHealth == false)
        {
            defencePointScript.organHealth -= 1;
            UpdateHealth();
            Debug.Log("Ow...");
            yield return new WaitForSeconds(1.0f);
        }

        stopDrainHealth = false;
    }

    void kill()
    {
        Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            bacteriaHealth = bacteriaHealth - bulletScript.damage;
            Debug.Log ("Hit dat mofucka");
        }
		if (other.tag == "DefencePoint")
		{
			StartCoroutine(drainHealth ()); 
			Debug.Log ("contact");
		}
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "DefencePoint")
		{
			stopDrainHealth = true;
			Debug.Log("stop");
		}
	}

    void UpdateHealth()
    {
        healthText.text = "Health: " + defencePointScript.organHealth;
    }
}
