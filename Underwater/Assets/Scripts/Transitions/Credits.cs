using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public float breathingFadeoutTime;
	private float timer;
	public AudioSource breathing;
	private float startingVolume;

	void Start()
	{
		startingVolume = breathing.volume;
		timer = breathingFadeoutTime;
	}

	// Update is called once per frame
	void Update() {
		timer -= Time.deltaTime;

		breathing.volume = startingVolume * timer / breathingFadeoutTime + 0.04f;


		if (Controls.JumpInputDown()) {
			//AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
			StartCoroutine(this.GetComponent<changeLevel>().change("mainMenu"));
			timer = Mathf.Min(timer, 2.0f);
		}
	}
}
