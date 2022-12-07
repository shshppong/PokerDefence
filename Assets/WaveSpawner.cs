using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokerDefence
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
                nextWaveCountText.text = _nextWaveCount.ToString();
            }
            _countdown -= Time.deltaTime;

            currentWaveText.text = Mathf.Floor(_countdown).ToString();
        }

        IEnumerator SpawnWave(float time)
        {
            yield return new WaitUntil(()=> ObjectPooler.GetAllPools("Enemy").Count >= _waveIndex);
            print("웨이브 당 한번만 실행되어야 합니다.");
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
