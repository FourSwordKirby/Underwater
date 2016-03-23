using UnityEngine;
using System.Collections;

public class IceDialogTrigger : MonoBehaviour {

    public TextAsset textFile;
    public Player.PlayerControls desiredControl;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner.weaponInventory.Find(x => x.name == "Ice") != null)
        {
            Player player = hurtbox.owner;

            //Make the player go into a dialog state + cutscenes I guess
            player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile, desiredControl));
            Destroy(this.gameObject);
        }
    }
}
