using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class weightsCheck : MonoBehaviour {

	public Player player;
	//public Sprite weightsImage;
	public Image weightsImage;
	public Sprite weight; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player.hasWeights == true) {
			weightsImage.sprite = weight;
		
		}
		if (!player.isWeighted) {
			weightsImage.color = new Color (weightsImage.color.r, weightsImage.color.b, weightsImage.color.g, .5f);
		} else
			weightsImage.color = new Color (weightsImage.color.r, weightsImage.color.b, weightsImage.color.g, 1.0f);
	}
}
