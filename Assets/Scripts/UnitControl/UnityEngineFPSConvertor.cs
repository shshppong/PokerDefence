using UnityEngine;

public class UnityEngineFPSConvertor : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
