using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerDefence
{
    public class WaveSpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        public Transform spawnPoint;

        public float timeBetweenWaves = 5f;

        [Range(0.0f, 1.0f)]
        public float waitForSecToSpawn = 0.5f;

        private float _countdown = 2f;

        [Range(1, 255)]
        public int _waveIndex = 1;
        
        void Update()
        {
            if(_countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                _countdown = timeBetweenWaves;
            }

            _countdown -= Time.deltaTime;
        }

        IEnumerator SpawnWave()
        {
            yield return new WaitUntil(()=> ObjectPooler.GetAllPools("Enemy").Count >= _waveIndex);
            // 괄호 안에 값이 참일 때 아래 구문 실행

            for(int i = 0; i < _waveIndex; i++)
            {
                // GameObject obj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                // ObjectPooler
                ObjectPooler.SpawnFromPool<Enemy>("Enemy", spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
