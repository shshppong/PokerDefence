using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class WaveSpawner : MonoBehaviour
    {
        [Header("적 유닛 프리팹")]
        public GameObject enemyPrefab;

        [Header("적 유닛 생성 위치")]
        public Transform spawnPoint;

        [Header("생성 되는 유닛의 시간 간격")]
        public float timeBetweenWaves = 140f;            // Default : 2m 20s (140s)

        [Header("다음 웨이브 대기시간")]
        [Range(0.0f, 1.0f)]
        public float SpawnWave_waitForSpawn = 0.5f;

        private float _countdown = 5f;

        [SerializeField]
        [Range(1, 255)]
        private int _waveIndex = 50;

        private List<GameObject> checkActiveEnemyObjectPoolers = new List<GameObject>();

        [Header("(UI) 현재 라운드")]
        public Text currentWaveText;                    // _countdown

        [Header("(UI) 다음 라운드까지 남은 시간")]
        public Text nextWaveCountText;

        [SerializeField]
        private int _nextWaveCount;

        [Header("일반 웨이브 마다 체력 배수")]
        [Range(1.1f, 5.0f)]
        public float hpSquare = 1.2f;

        
        [Header("보스 웨이브 마다 체력 배수")]
        [Range(2.0f, 5.0f)]
        public float bossHpSquare = 2.0f;
        
        void Start()
        {
            checkActiveEnemyObjectPoolers.AddRange(ObjectPooler.GetAllPools("Enemy"));

            _nextWaveCount = 0;
        }

        void Update()
        {
            if(_countdown <= 0f)
            {
                StartCoroutine(SpawnWave(SpawnWave_waitForSpawn));
                _countdown = timeBetweenWaves;
                _nextWaveCount++;
                nextWaveCountText.text = "현재 웨이브: " + _nextWaveCount.ToString();
            }
            _countdown -= Time.deltaTime;

            currentWaveText.text = "다음 웨이브까지 " + Mathf.Floor(_countdown) + "초 전".ToString();
        }

        IEnumerator SpawnWave(float time)
        {
            yield return new WaitUntil(()=> ObjectPooler.GetAllPools("Enemy").Count >= _waveIndex);

            for(int i = 0; i < _waveIndex; i++)
            {
                // GameObject obj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                // ObjectPooler
                ObjectPooler.SpawnFromPool<Enemy>("Enemy", spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(time);

                int checkCount = CheckActiveObjects();
                if(checkCount >= 150)
                {
                    print("GameOver");
                }
                else if(checkCount >= 100)
                {
                    print("적 유닛이 100기 이상입니다.");
                }
            }
        }

        // 프로파일러 확인하니까 이거 성능 좀 깍아먹는 것 같다.
        int CheckActiveObjects()
        {
            int cnt = 0;
            for(int i = 0; i < checkActiveEnemyObjectPoolers.Count; i++)
            {
                if(checkActiveEnemyObjectPoolers[i].gameObject.activeSelf)
                    cnt++;
            }
            return cnt;
        }
    }
}
