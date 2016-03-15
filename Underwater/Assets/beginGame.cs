using UnityEngine;
using System.Collections;

public class beginGame : MonoBehaviour {
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown ("z")) {
			print ("z key was pressed");
			Application.LoadLevel("Submarine");

		}
	}
		

}
