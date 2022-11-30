using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefenseAssets
{
    public class WaveSpawner0 : MonoBehaviour
    {
        public Transform enemyPrefab;

        public Transform spawnPoint;

        public float timeBetweenWaves = 5f;
        private float countdown = 2f;

        private int waveIndex = 1;

        public Text waveCountdownText;

        void Update()
        {
            if(countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Floor(countdown).ToString();
        }

        IEnumerator SpawnWave()
        {
            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }

            waveIndex++;
        }
        void SpawnEnemy()  
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
