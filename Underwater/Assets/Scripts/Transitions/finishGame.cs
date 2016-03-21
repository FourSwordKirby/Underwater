using UnityEngine;
using System.Collections;

public class finishGame : MonoBehaviour {

    public void startCredits()
    {
        //AutoFade.LoadLevel("Submarine", 3, 1, Color.black);
        StartCoroutine(this.GetComponent<changeLevel>().change("credits", 0.0f));
    }
}
