using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class WaveSpawnerV2 : MonoBehaviour
    {
        public static int EnemiesAlive = 0;

        public WaveSpawner[] waves;

        public Transform spawnPoint;

        public float timeBetweenWaves = 5f;
        private float countdown = 2f;

        public Text waveCountdownText;

        private int waveIndex = 0;

        void Update()
        {
            if(EnemiesAlive > 0)
            {
                return;
            }
        }
    }
}
