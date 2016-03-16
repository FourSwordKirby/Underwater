using UnityEngine;
using System.Collections;

public class CameraBoundTransition : MonoBehaviour {

    /*self-references*/
    public BoxCollider2D newCameraBounds;

    void OnTriggerEnter2D()
    {
        Camera myCamera = Camera.main;
        myCamera.GetComponent<CameraControls>().CameraBounds = newCameraBounds;
    }
}
