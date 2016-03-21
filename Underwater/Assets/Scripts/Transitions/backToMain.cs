using UnityEngine;
using System.Collections;

public class backToMain : MonoBehaviour {
	// Update is called once per frame
	void Update() {
		if (Controls.JumpInputDown()) {
			//AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
			StartCoroutine(this.GetComponent<changeLevel>().change("mainMenu"));
		}
	}
}
