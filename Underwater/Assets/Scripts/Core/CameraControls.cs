using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraControls : MonoBehaviour {

    private Camera cameraComponent;

    public FocalPoint focus;
    public BoxCollider2D CameraBounds;
    public BoxCollider2D desiredCameraBounds;

    private List<GameObject>visibleTargets; //accounts for all other targets that aren't just the main focus

    private float original_camera_size;
    private float min_camera_size;
    private float max_camera_size;
    private float target_camera_size;

    public float zoomSpeed;
    public float maxZoomFOV;

    /* camera moving constants */
    private const float Z_OFFSET = -10;

    /* A bunch of stuff that relates to how the camera shakes*/
    public enum ShakePresets
    {
        NONE,
        BOTH,
        HORIZONTAL,
        VERTICAL
    };
    private float shakeIntensity = 0.0f;
    private float shakeDuration = 0.0f;
    private ShakePresets shakeDirection = ShakePresets.BOTH;
    private Action shakeComplete = null;
    private Vector2 shakeOffset = new Vector2();

    /*CONSTANTS*/
    private const float TARGETING_LOWER_BOUND = 0.0f;
    private const float TARGETING_UPPER_BOUND = 1.0f;
    private const float ZOOM_IN_LOWER_BOUND = 0.3f;
    private const float ZOOM_IN_UPPER_BOUND = 0.7f;
    private const float ZOOM_OUT_LOWER_BOUND = 0.2f;
    private const float ZOOM_OUT_UPPER_BOUND = 0.8f;

    private const float ZOOM_RATE = 0.02f;

    private const float PAN_SPEED = 5.0f;

	// Use this for initialization
	void Start () {
        cameraComponent = GetComponent<Camera>();

        original_camera_size = cameraComponent.orthographicSize;
        min_camera_size = 1.0f * original_camera_size;
        max_camera_size = 2.0f * original_camera_size;
        target_camera_size = original_camera_size;

        CameraBounds.transform.position = desiredCameraBounds.transform.position;
        CameraBounds.transform.localScale = desiredCameraBounds.transform.localScale;

        transform.position = focus.transform.position + new Vector3(0, 0, Z_OFFSET);
	}
	
	void FixedUpdate () {
        //Now follow the target
        if (transform.position != focus.transform.position + new Vector3(0, 0, Z_OFFSET))
        {
            float x = ((focus.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).x;
            float y = ((focus.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).y;
            GetComponent<Rigidbody2D>().velocity = new Vector2(x * PAN_SPEED, y * PAN_SPEED);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity.Set(0.0f, 0.0f);
        }

        //Do shake calculations
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0)
                stopShaking();
            else
                applyShake();
        }

        //Keep the camera in bounds
        Vector3 pos;
        pos.x = Mathf.Clamp(transform.position.x, CameraBounds.bounds.min.x + cameraComponent.orthographicSize * Screen.width / Screen.height,
                                                  CameraBounds.bounds.max.x - cameraComponent.orthographicSize * Screen.width / Screen.height);
        pos.y = Mathf.Clamp(transform.position.y, CameraBounds.bounds.min.y + cameraComponent.orthographicSize,
                                                  CameraBounds.bounds.max.y - cameraComponent.orthographicSize);
        pos.z = transform.position.z;
        transform.position = pos;

        //used to scale the camera's size as targets spread out
        /*foreach(Mobile targ in focus.targets)
        {
            //check that the target is in bounds
            if(inCameraBounds(targ))
            {
                resizeToFitTarget(targ);
            }
        }
         */
        //external gradual resizing
        float cameraSize = cameraComponent.orthographicSize;
        if (cameraSize < target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 + ZOOM_RATE, zoomSpeed * Time.deltaTime);
        if (cameraSize > target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 - ZOOM_RATE, zoomSpeed * Time.deltaTime);

        if (Math.Abs(cameraComponent.orthographicSize - target_camera_size) < 0.01f)
        {
            cameraComponent.orthographicSize = target_camera_size;
        }

        //adjusting camera bound
        CameraBounds.transform.position = Vector3.Lerp(CameraBounds.transform.position, desiredCameraBounds.transform.position, 1.0f * Time.deltaTime);
        CameraBounds.transform.localScale = Vector3.Lerp(CameraBounds.transform.localScale, desiredCameraBounds.transform.localScale, 1.0f * Time.deltaTime);

        if (Math.Abs(CameraBounds.transform.position.x - desiredCameraBounds.transform.position.x) < 1f
            && Math.Abs(CameraBounds.transform.position.y - desiredCameraBounds.transform.position.y) < 1f) 
        {
            CameraBounds.transform.position = desiredCameraBounds.transform.position;
        }

        if (Math.Abs(CameraBounds.transform.localScale.x - desiredCameraBounds.transform.localScale.x) < 1f
            && Math.Abs(CameraBounds.transform.localScale.y - desiredCameraBounds.transform.localScale.y) < 1f)
        {
            CameraBounds.transform.localScale = desiredCameraBounds.transform.localScale;
        }
    }

    public void Shake(float Intensity = 0.05f, 
                        float Duration = 0.5f, 
                        Action OnComplete = null, 
                        bool Force = true, 
                        ShakePresets Direction = ShakePresets.BOTH)
    {
        if(!Force && ((shakeOffset.x != 0) || (shakeOffset.y != 0)))
			return;
		shakeIntensity = Intensity;
		shakeDuration = Duration;
        shakeComplete = OnComplete;
		shakeDirection = Direction;
        shakeOffset.Set(0, 0);
    }

    private void stopShaking()
    {
        shakeOffset.Set(0, 0);
        if (shakeComplete != null)
            shakeComplete();
    }

    private void applyShake()
    {
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.HORIZONTAL)
            shakeOffset.x = (UnityEngine.Random.Range(-1.0f, 1.0f) * shakeIntensity);
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.VERTICAL)
            shakeOffset.y = (UnityEngine.Random.Range(-1.0f, 1.0f) * shakeIntensity);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x + shakeOffset.x, y + shakeOffset.y, z);
        Debug.Log(shakeOffset.x);
        Debug.Log(shakeOffset.y);
        Debug.Log(transform.position);
    }

    public void repositionCameraBound(BoxCollider2D targetBound)
    {
        desiredCameraBounds = targetBound;
    }

    public void changeCameraSize(float desiredSize)
    {
        target_camera_size = desiredSize;
    }
    
    public void resetCameraSize()
    {
        target_camera_size = original_camera_size;
    }

    private bool inCameraBounds(Mobile target)
    {
        return (CameraBounds.bounds.min.x < target.transform.position.x && target.transform.position.x < CameraBounds.bounds.max.x
            && CameraBounds.bounds.min.y < target.transform.position.y && target.transform.position.y < CameraBounds.bounds.max.y);
    }

    private void resizeToFitTarget(Mobile target)
    {
        float originalSize = cameraComponent.orthographicSize;
        float calculatedSize = originalSize;

        Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);

        while (!(ZOOM_OUT_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_OUT_UPPER_BOUND
              && ZOOM_OUT_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_OUT_UPPER_BOUND))
        {
            if (calculatedSize < max_camera_size)
            {
                cameraComponent.orthographicSize *= 1+ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        while (ZOOM_IN_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_IN_UPPER_BOUND
            && ZOOM_IN_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_IN_UPPER_BOUND)
        {
            if (calculatedSize > min_camera_size)
            {
                cameraComponent.orthographicSize *= 1-ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        cameraComponent.orthographicSize = originalSize;
        target_camera_size = calculatedSize;
    }
}
