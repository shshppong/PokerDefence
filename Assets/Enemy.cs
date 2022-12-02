using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerDefence
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10f;

        private Transform _target;

        [SerializeField]
        private int _wayPointIndex;

        void OnDisable()
        {
            ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
            CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
        }

        void Start()
        {
            _target = Waypoints.Points[0];
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
    }
}
