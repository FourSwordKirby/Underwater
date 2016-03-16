﻿using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;

    private float fadeTime;

    void Update()
    {
        if (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            Debug.Log(fadeTime);
        }
    }
    /*
	void onGUI(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b,alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height),fadeOutTexture);
	}
     */

	public void beginFade (float time){
        fadeTime = time;
	}

	void onLevelWasLoaded(){
		beginFade (-1);
	}



}
