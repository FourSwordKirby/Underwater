﻿using UnityEngine;
using System.Collections;

public class changeLevel : MonoBehaviour {

    public IEnumerator change(string levelName)
    {
        float fadeTime = 0.8f;
        this.GetComponent<ScreenFader>().fadeIn = false;
        yield return this.GetComponent<ScreenFader>().FadeOut();
        Application.LoadLevel(levelName);
   	}
}
