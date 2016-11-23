using UnityEngine;
using System.Collections;

public class DefencePoint : MonoBehaviour {

	public float organHealth;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (organHealth <= 0) {
		}
	
	}


	/*void onGUI(){

		GUI.Label(new Rect(10,10,150,100), "Health ="+ organHealth);
	}*/
}
