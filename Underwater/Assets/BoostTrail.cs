using UnityEngine;
using System.Collections;

public class BoostTrail : MonoBehaviour {

    public float decayTime;

    void Update()
    {
        decayTime -= Time.deltaTime;
        if (decayTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
