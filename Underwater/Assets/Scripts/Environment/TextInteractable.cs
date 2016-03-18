using UnityEngine;
using System.Collections;

public class TextInteractable : Interactable
{

    public TextAsset textFile;
    public Player.PlayerControls desiredControl;

    override public void Interact(Player player)
    {
        //Make the player go into a dialog state + cutscenes I guess
		if(player.ActionFsm.CurrentState.ToString() != "DialogState")
	        player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile, desiredControl));
    }
}