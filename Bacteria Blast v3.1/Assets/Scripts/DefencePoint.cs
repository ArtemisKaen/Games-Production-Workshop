using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefencePoint : MonoBehaviour {

	public int organStartHealth = 100;
    public int organHealth;
    public Slider healthSlider;
	
	// Use this for initialization
	void Start () {
        organHealth = organStartHealth;
        healthSlider.value = organHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (organHealth <= 0) {
            SceneManager.LoadScene ("Death screen");
            healthSlider.value = 100;
		}
	}


	/*void onGUI(){

		GUI.Label(new Rect(10,10,150,100), "Health ="+ organHealth);
	}*/
}
