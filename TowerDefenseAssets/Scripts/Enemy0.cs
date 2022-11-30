using UnityEngine;

namespace TowerDefenseAssets
{
    public class Enemy0 : MonoBehaviour
    {
        public float speed = 10f;

        private Transform target;
        private int wavepointIndex = 0;

        void Start()
        {
            target = Waypoints.points[0];
        }

        void Update()
        {
            Vector3 dir = target.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
            if(Vector3.Distance(target.position, transform.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
        }

        void GetNextWaypoint()
        {
            if(wavepointIndex >= Waypoints.points.Length - 1)
            {
                Destroy(gameObject);
                return;
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }
    }
}