using UnityEngine;
using System.Collections;

public class BossSpecificTransition : MonoBehaviour {

    public GameObject Wall;
    public Boss levelBoss;

    void OnTriggerEnter2D()
    {
        Wall.SetActive(true);
        levelBoss.activated = true;
        Debug.Log("THE EXALTED");
    }
}
