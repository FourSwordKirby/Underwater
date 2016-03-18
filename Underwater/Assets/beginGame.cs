using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class beginGame : MonoBehaviour {
    public Text gameText;

	// Update is called once per frame
	void Update() {
		if (Controls.JumpInputDown()) {
            //AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
            gameText.text = "Loading...";
            StartCoroutine(this.GetComponent<changeLevel>().change("Contiguous"));
		}
	}
}
