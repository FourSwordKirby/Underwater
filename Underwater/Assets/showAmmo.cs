using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showAmmo : MonoBehaviour {

	public Text ammoNum;
	public Player player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (player.activeWeapon != null) {
			ammoNum.text = player.activeWeapon.GetComponent<Weapon> ().GetAmmoCount ().ToString();
		} else
			ammoNum.text = "";
	
	}
}
