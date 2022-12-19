using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace HwatuDefence
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitProcess : MonoBehaviour
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

        public float turnSpeed = 10f;

        public GameObject bulletPrefab;
        public Transform firePoint;
        public RectTransform rectTr;

        public UnitController unitController;

        private NavMeshAgent navAgent;

        public UnitData data;

        public Text unitNameText;
        
        private void LoadUnit(UnitData _data)
        {
            GameObject visuals = Instantiate(data.unitModel);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = Vector3.zero;
            visuals.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            if(navAgent == null)
                navAgent = this.GetComponent<NavMeshAgent>();

            navAgent.speed = _data.moveSpeed;
        }

        void Update()
        {
            // 범위 내 타겟 추출
            UpdateTarget();

            if(target == null) return;
            if (unitController.IsActiveDestination()) return; // 이동 중이면 공격하지 않기
            
            Vector3 dir = target.position - firePoint.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            firePoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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
        public void SetUnitAttackValues(int attack, int cardNum)
        {
            if(cardNum == 0)
                this.unitName = data.unitName;
            else
                this.unitName = cardNum + data.unitName;

            this.attack = data.attack + attack; // 추가 공격력
            this.attackSpeed *= data.attackSpeed;
            this.attackRange *= data.attackRange;
            this.moveSpeed *= data.moveSpeed;
            this.bulletPrefab = data.bulletModel;

            float m_scale = this.attackRange/100;
            rectTr.localScale = new Vector3(m_scale, m_scale, m_scale);
            
            unitNameText.text = this.unitName.ToString();
        }

        public void SetUnitSO(UnitData so)
        {
            data = so;
            LoadUnit(data);
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
