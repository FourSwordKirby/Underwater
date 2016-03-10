using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {
    /// <summary>
    /// This is the function that gets called when the player hit's interact
    /// </summary>
    /// <param name="player"></param>
    public abstract void Interact(Player player);

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject);
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.owner.currentInteractable = this;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner.currentInteractable == this)
        {
            hurtbox.owner.currentInteractable = null;
        }
    }
}
