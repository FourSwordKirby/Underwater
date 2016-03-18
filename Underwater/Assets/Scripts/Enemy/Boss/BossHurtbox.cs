using UnityEngine;
using System.Collections;

public class BossHurtbox : MonoBehaviour
{
    public Boss owner;

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bomb>() != null)
        {
            owner.TakeDamage(1);
            this.gameObject.SetActive(false);
        }
    }
}
