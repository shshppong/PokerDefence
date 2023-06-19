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

        [Header("적 보스 프리팹")]
        public GameObject bossPrefab;

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
        public static int _nextWaveCount;

        [Header("(UI) 필드에 있는 적 개수")]
        public Text enemyCountText;

        [Header("100마리 이상 경고 이미지")]
        public GameObject warningImage;

        [Header("일반 웨이브 마다 체력 배수")]
        [Range(1.1f, 5.0f)]
        public float hpSquare = 1.2f;

        
        [Header("보스 웨이브 마다 체력 배수")]
        [Range(2.0f, 5.0f)]
        public float bossHpSquare = 2.0f;

        private int hpE = 10; // hp Enemy
        private int hpB = 100; // hp Boss
        
        void Start()
        {
            checkActiveEnemyObjectPoolers.AddRange(ObjectPooler.GetAllPools("Enemy"));

            _nextWaveCount = 0;

            hpE = 10;
            hpB = 650;
            // (x * 5 * 1.5 * 100) - (x * 100 * 1.25)
            // (현재웨이브 * 시작체력 * 배수 * 보스체력추가배수) - (현재웨이브 * 체력보정 * 보정배수)
        }

        void Update()
        {
            if(_countdown <= 0f)
            {
                StartCoroutine(SpawnWave(SpawnWave_waitForSpawn));
                _countdown = timeBetweenWaves;
                _nextWaveCount++;
                nextWaveCountText.text = _nextWaveCount + "\n" + "라운드".ToString();

                hpE = (int)(_nextWaveCount * 10 * hpSquare);
                print(hpE);
                
                foreach(Enemy e in ObjectPooler.GetAllPools<Enemy>("Enemy"))
                {
                    e.startHp = hpE;
                }

                // 만약 nextWaveCountText % 10 == 0 일 경우 보스몹을 소환시킨다.
                // 각 보스는 10 스테이지마다 소환 보스 스테이지에서는 적 유닛이 나오지 않음
            }
            _countdown -= Time.deltaTime;

            currentWaveText.text = string.Format("{0:00.00}", _countdown) + "\t초".ToString();

            int check = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyCountText.text = "적 유닛\n" + check + "\t 기".ToString();

            if(_nextWaveCount >= 100)
            {  
                GameManager.Inst.GameOver();
            }
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
                    GameManager.Inst.GameOver();
                }
                else if(checkCount >= 100)
                {
                    warningImage.SetActive(true);
                    print("적 유닛이 100기 이상입니다.");
                }
                else if(warningImage.activeSelf == true)
                {
                    warningImage.SetActive(false);
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
