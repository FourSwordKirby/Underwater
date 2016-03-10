﻿using UnityEngine;
using System.Collections;

public class WeightPowerup : MonoBehaviour 
{
    public TextAsset textFile;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner;
            player.hasWeights = true;

            //Make the player go into a dialog state + cutscenes I guess
            player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile));

            Destroy(this.gameObject);
        }
    }
}
