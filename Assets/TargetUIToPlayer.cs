using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIToPlayer : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(cam.transform);
    }
}
