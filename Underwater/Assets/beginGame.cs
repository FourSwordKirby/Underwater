using UnityEngine;
using System.Collections;

public class beginGame : MonoBehaviour {
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown ("z")) {
			print ("z key was pressed");
            //AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
            StartCoroutine(this.GetComponent<changeLevel>().change("Submarine"));
		}
	}
}
