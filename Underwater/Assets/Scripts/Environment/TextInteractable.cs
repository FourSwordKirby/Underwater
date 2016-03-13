﻿using UnityEngine;
using System.Collections;

public class TextInteractable : Interactable
{

    public TextAsset textFile;

    override public void Interact(Player player)
    {
        //Make the player go into a dialog state + cutscenes I guess
        player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile));
        Destroy(this.gameObject);
    }
}