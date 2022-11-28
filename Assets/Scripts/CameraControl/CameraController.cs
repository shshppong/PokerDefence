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
    
    void Update()
    {
        InputKeyCameraMove();
    }

    void InputKeyCameraMove()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        
        Camera.main.transform.position = pos;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawLine(posA.position, );
    }

}
