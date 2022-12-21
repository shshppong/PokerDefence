using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 10f;

    public float fovStart = 60f;
    public float fovEnd = 30f;
    public float transitionTime = 3f;

    private Camera _camera;

	public	RTSUnitController rtsUnitController;
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
        if(rtsUnitController.ReturnGameObject() == null) return;
        
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
