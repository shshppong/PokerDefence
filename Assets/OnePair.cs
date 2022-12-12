using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class OnePair : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        public float range = 2.5f;
        public float fireRate = 1f;
        private float fireCountdown = 0f;

        public string enemyTag = "Enemy";
        
        public float turnSpeed = 10f;

        public GameObject bulletPrefab;
        public Transform firePoint;
        public Image indicator;

        public UnitController unitController;

        void Start()
        {
            // 초기 세팅
            indicator.transform.localScale = new Vector3(range, range, range);
            
            float timer = 1f / fireRate;
            InvokeRepeating("UpdateTarget", 0f, timer);
        }

        void UpdateTarget()
        {
            target = null;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = range;
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

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
        }

        void Update()
        {
            if(target == null) return;
            if (unitController.IsActiveDestination()) return;

            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
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

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
