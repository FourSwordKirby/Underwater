﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckWeapon : MonoBehaviour {
	public Player player;
	public Image ammoImage;
	public List<Sprite> spriteArray;
	public int ammo;




	// make array of sprite images here 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.activeWeapon == null) {
			ammoImage.sprite = spriteArray[0];
		}
		else if (player.activeWeapon.GetComponent<Bubbler>() != null) {
			ammoImage.sprite = spriteArray [1];
			ammo = player.activeWeapon.GetComponent<Bubbler> ().GetAmmoCount();
		}
		else if(player.activeWeapon.GetComponent<Bomber>() != null){
			ammoImage.sprite = spriteArray [2];
			ammo = player.activeWeapon.GetComponent<Bomber>().GetAmmoCount();
		}
			

			
	}
}