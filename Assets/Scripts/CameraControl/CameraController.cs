using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 10f;
    [SerializeField]
    private float minY = 10f;
    [SerializeField]
    private float maxY = 80f;

    public float fovStart = 60f;
    public float fovEnd = 30f;
    public float transitionTime = 3f;

    private float _currentFov;
    private float _lerpTime;
    private Camera _camera;

	private	RTSUnitController rtsUnitController;
    private Vector3 worldDefaultForward;

    void Start() {
        _camera = this.GetComponent<Camera>();

        worldDefaultForward = transform.forward;
    }
    
    void FixedUpdate()
    {
        InputKeyCameraMove();
        // ChangeFOV();
    }

    void InputKeyCameraMove()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed * 10f;
        
        // Vector3 pos = transform.position;

        // pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // pos.y = Mathf.Clamp(pos.y, minY, maxY);
        
        // Camera.main.transform.position = pos;

        if(_camera.fieldOfView <= 20f && scroll < 0)
        {
            _camera.fieldOfView = 20f;
        }
        else if(_camera.fieldOfView >= 60f && scroll > 0)
        {
            _camera.fieldOfView = 60f;
        }
        else
        {
            _camera.fieldOfView += scroll;
        }

        if(rtsUnitController.ReturnGameObject() != null) // 오류
        {
            Transform cameraTarget = rtsUnitController.ReturnGameObject().transform;
            if(cameraTarget && _camera.fieldOfView <= 30f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraTarget.position - transform.position), 0.15f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(worldDefaultForward), 0.15f);
            }
        }
    }

    // void ChangeFOV()
    // {
    //     if(Mathf.Abs(_currentFov - fovEnd) > float.Epsilon)
    //     {
    //         _lerpTime += Time.deltaTime;
    //         float t = _lerpTime / transitionTime;

    //         t = Mathf.SmoothStep(0, 1, t);
    //         _currentFov = Mathf.Lerp(fovStart, fovEnd, t);
    //     }
    //     else if(Mathf.Abs(_currentFov - fovEnd) < float.Epsilon)
    //     {
    //         _lerpTime = 0;
    //         float tmp = fovStart;
    //         fovStart = fovEnd;
    //         fovEnd = tmp;
    //     }

    //     _camera.fieldOfView = _currentFov;
    // }

    // private float SmootherStep(float t)
    // {
    //     return t * t * t * (t * (6f * t - 15f) + 10f);
    // }

}
