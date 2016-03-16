using UnityEngine;
using System.Collections;

public class CameraBoundTransition : MonoBehaviour {

    /*self-references*/
    public BoxCollider2D newCameraBounds;
    public float cameraSize;


    void OnTriggerEnter2D()
    {
        Camera myCamera = Camera.main;
        myCamera.GetComponent<CameraControls>().repositionCameraBound(newCameraBounds);
        myCamera.GetComponent<CameraControls>().changeCameraSize(cameraSize);
    }
}
