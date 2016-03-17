using UnityEngine;
using System.Collections;

public class BossSpecificTransition : MonoBehaviour {

    public GameObject Wall;
    public GameObject Boss;

    void OnTriggerEnter2D()
    {
        Wall.SetActive(true);
        Boss.SetActive(true);
        Debug.Log("THE EXALTED");
    }
}
