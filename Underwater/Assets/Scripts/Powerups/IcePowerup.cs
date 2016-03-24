using UnityEngine;
using System.Collections;

public class IcePowerup : Powerup
{
    public Weapon IceBubbler;

    public TextAsset textFile;

    public AudioSource pickupSound;

    public GameObject wall;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner; 
            player.AddWeapon(IceBubbler);

            //Make the player go into a dialog state + cutscenes I guess
            player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile, Player.PlayerControls.Aim));

            pickupSound.Play();

            finishPickup();

            wall.SetActive(false);
        }
    }
}
