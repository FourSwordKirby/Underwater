using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {

	public void gameOver()
	{
		//AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
		StartCoroutine(this.GetComponent<changeLevel>().change("gameOver", 0.0f));
	}
}
