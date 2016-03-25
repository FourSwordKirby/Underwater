using UnityEngine;
using System.Collections;

public class BossHurtbox : MonoBehaviour
{
    public Boss owner;

    public float cooldown;

    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bomb>() != null)
        {
            Destroy(col.gameObject);
            if (cooldown > 0)
                return;
            else
            {
                cooldown = 3.0f;
                owner.TakeDamage(1);
            }
        }
    }
}
