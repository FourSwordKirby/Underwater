using UnityEngine;
using System.Collections;

public class CameraBoundTransition : MonoBehaviour {

    /*self-references*/
    public BoxCollider2D newCameraBounds;
    public float cameraSize;

    public AudioSource newAudio;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            Camera myCamera = Camera.main;
            myCamera.GetComponent<CameraControls>().repositionCameraBound(newCameraBounds);
            myCamera.GetComponent<CameraControls>().changeCameraSize(cameraSize);
            AudioController.changeAudioSource(newAudio, newAudio.volume);
        }
    }
}
