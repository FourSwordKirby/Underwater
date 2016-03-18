using UnityEngine;
using System.Collections;

public class changeLevel : MonoBehaviour {

	public IEnumerator change(string levelName, float time = 0.8f)
    {
		float fadeTime = time;
        this.GetComponent<ScreenFader>().fadeIn = false;
        yield return this.GetComponent<ScreenFader>().FadeOut();
        Application.LoadLevel(levelName);
   	}
}
