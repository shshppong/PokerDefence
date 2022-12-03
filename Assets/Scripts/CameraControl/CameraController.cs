using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform posA;
    [SerializeField]
    private Transform posB;
    [SerializeField]
    AnimationCurve animCurve;

    [SerializeField]
    private float scrollSpeed = 5f;
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

    void Start() {
        _camera = this.GetComponent<Camera>();
    }
    
    void FixedUpdate()
    {
        InputKeyCameraMove();
        // ChangeFOV();
    }

    void InputKeyCameraMove()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        
        Camera.main.transform.position = pos;
    }

    void ChangeFOV()
    {
        if(Mathf.Abs(_currentFov - fovEnd) > float.Epsilon)
        {
            _lerpTime += Time.deltaTime;
            float t = _lerpTime / transitionTime;

            t = Mathf.SmoothStep(0, 1, t);
            _currentFov = Mathf.Lerp(fovStart, fovEnd, t);
        }
        else if(Mathf.Abs(_currentFov - fovEnd) < float.Epsilon)
        {
            _lerpTime = 0;
            float tmp = fovStart;
            fovStart = fovEnd;
            fovEnd = tmp;
        }

        _camera.fieldOfView = _currentFov;
    }

    // private float SmootherStep(float t)
    // {
    //     return t * t * t * (t * (6f * t - 15f) + 10f);
    // }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(posA.position, posB.position);
    }

}
