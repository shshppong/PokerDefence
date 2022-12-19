using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HwatuDefence
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class GWANGTTAENG : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private string unitName = "";
        [SerializeField]
        private int attack = 1;
        [SerializeField]
        private float attackSpeed = 1f;
        private float fireCountdown = 0f;
        [SerializeField]
        private float attackRange = 1f;
        [SerializeField]
        private float moveSpeed = 1f;
        
        public string enemyTag = "Enemy";

        public GameObject bulletPrefab;
        public Transform firePoint;
        public RectTransform rectTr;

        public UnitController unitController;

        private NavMeshAgent navAgent;
        public UnitData data;

        void Start()
        {
            if(data != null)
                LoadUnit(data);

            // 인스턴스 접근
            SetUnitValues(data.unitName, data.attack, data.attackSpeed, data.attackRange, data.moveSpeed);
        }

        private void LoadUnit(UnitData _data)
        {
            foreach(Transform child in this.transform)
            {
                if(Application.isEditor)
                    DestroyImmediate(child.gameObject);
                else
                    Destroy(child.gameObject);
            }

            GameObject visuals = Instantiate(data.unitModel);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = Vector3.zero;
            visuals.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            if(navAgent == null)
                navAgent = this.GetComponent<NavMeshAgent>();
            
            this.navAgent.speed = data.moveSpeed;
        }

        void Update()
        {
            // 범위 내 타겟 추출
            UpdateTarget();

            if(target == null) return;
            if (unitController.IsActiveDestination()) return;

            // 바라보기
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // 발사 속도
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / attackSpeed;
            }
            fireCountdown -= Time.deltaTime;
        }

        void UpdateTarget()
        {
            target = null;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = attackRange;
            GameObject nearestEnemy = null;
            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= attackRange)
            {
                target = nearestEnemy.transform;
            }
        }

        void Shoot()
        {
            GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGo.GetComponent<Bullet>();

            if(bullet != null)
            {
                bullet.Seek(target);
            }
        }

        // 스크립터블 오브젝트 데이터에서 가져오기
        public void SetUnitValues(string unitName, int attack, float attackSpeed, float attackRange, float moveSpeed)
        {
            this.unitName = unitName;
            this.attack = attack;
            this.attackSpeed *= attackSpeed;
            this.attackRange *= attackRange;
            this.moveSpeed *= moveSpeed;

            float m_scale = this.attackRange/100;
            rectTr.localScale = new Vector3(m_scale, m_scale, m_scale);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
#endif
    }
}
