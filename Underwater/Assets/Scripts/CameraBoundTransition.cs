using UnityEngine;
using System.Collections;

public class CameraBoundTransition : MonoBehaviour {

    /*self-references*/
    public Collider2D newCameraBounds;

    void OnTriggerEnter2D()
    {
        Camera myCamera = Camera.main;
    }
}
