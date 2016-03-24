using UnityEngine;
using System.Collections;

public class BossSpecificTransition : MonoBehaviour {

    public GameObject Wall;
    public Boss levelBoss;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox player = col.GetComponent<PlayerHurtbox>();
        if (player != null)
        {
            Wall.SetActive(true);
            levelBoss.activated = true;
            Debug.Log("THE EXALTED");
        }
    }
}
