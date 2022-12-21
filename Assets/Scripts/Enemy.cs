using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class Enemy : MonoBehaviour
    {
        private Transform _target;

        [SerializeField]
        private int _wayPointIndex;

        [SerializeField]
        private UnitData data;
        
        public GameObject deathEffect;

        [Header("체력 이미지")]
        public Image healthBar;

        private int money = 1;

        // 스텟
        public int startHp = 10;
        private int hp;
        public float StartSpeed = 8f;
        private float speed;

        void OnDisable()
        {
            ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
            CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
        }

        void Start()
        {
            _target = Waypoints.Points[0];
            speed = StartSpeed; 
            hp = startHp;
        }

        void Update()
        {
            Vector3 dir = _target.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
            if(Vector3.Distance(_target.position, transform.position) <= 0.4f)
            {
                GetNextWayPoint();
            }
        }

        private void GetNextWayPoint()
        {
            if(_wayPointIndex >= Waypoints.Points.Length - 1)
            {
                _wayPointIndex = -1;
                return;
            }

            _wayPointIndex++;
            _target = Waypoints.Points[_wayPointIndex];
        }

        public void TakeDamage(int amount)
        {
            hp -= amount;

            healthBar.fillAmount = (float)hp / startHp;

            if(hp <= 0)
            {
                Die();
            }
        }
        
        void Die()
        {
            // 1킬당 포인트 1
            PlayerStats.Money += money;

            GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            
            gameObject.SetActive(false);
        }
    }
}
