using UnityEngine;
using System.Collections;

public class BombPowerup : Powerup {

    public Weapon TimedBomber;

    public TextAsset textFile;

    public AudioSource pickupSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner;
            player.AddWeapon(TimedBomber);

            //Make the player go into a dialog state + cutscenes I guess
            player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile));

            pickupSound.Play();

            finishPickup();
        }
    }
}
