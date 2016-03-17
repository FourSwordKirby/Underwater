using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckWeapon : MonoBehaviour {
	public Player player;
	public Image ammoImage;
	public List<Sprite> spriteArray;
    public int ammo;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.activeWeapon == null || player.activeWeapon.gameObject.activeSelf == false) {
            ammoImage.enabled = false;
		}
		else if (player.activeWeapon.GetComponent<Bubbler>() != null) {
            if (player.activeWeapon.GetComponent<Bubbler>().ammo.effect != Parameters.DamageEffect.Freeze)
            {
                ammoImage.sprite = spriteArray[0];
                ammo = player.activeWeapon.GetComponent<Bubbler>().GetAmmoCount();
            }
            else
            {
                ammoImage.sprite = spriteArray[1];
                ammo = player.activeWeapon.GetComponent<Bubbler>().GetAmmoCount();
            }
        }
		else if(player.activeWeapon.GetComponent<Bomber>() != null){
			ammoImage.sprite = spriteArray [2];
			ammo = player.activeWeapon.GetComponent<Bomber>().GetAmmoCount();
		}
			

			
	}
}
