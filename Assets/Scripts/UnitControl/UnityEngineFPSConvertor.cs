using UnityEngine;

public class UnityEngineFPSConvertor : MonoBehaviour
{
    [Header("게임 배속 조절")]
    [Range(1, 4)]
    public int gameSpeed = 1;            // Default : 1x (1배속)

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        Time.timeScale = gameSpeed;
    }
}
