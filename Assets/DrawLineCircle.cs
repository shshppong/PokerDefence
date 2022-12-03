using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;

    void Start()
    {
        DrawCircle(110, 1);
    }

    void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep/steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, transform.position.z);
            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }
}
