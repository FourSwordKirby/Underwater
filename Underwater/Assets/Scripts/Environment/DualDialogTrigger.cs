using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DualDialogTrigger : MonoBehaviour {

    public TextAsset textFile;
    public Player.PlayerControls desiredControl;
    public List<GameObject> associatedObjects;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner;

            //Make the player go into a dialog state + cutscenes I guess
            player.ActionFsm.ChangeState(new DialogState(player, player.ActionFsm, textFile, desiredControl));
            foreach(GameObject obj in associatedObjects)
            {
                Destroy(obj);
            }
            Destroy(this.gameObject);
        }
    }
}
