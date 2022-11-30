using UnityEngine;

namespace TowerDefenseAssets
{
    public class Bullet0 : MonoBehaviour
    {
        private Transform target;

        public float speed = 70f;
        public GameObject impactEffect;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        void Update()
        {
            if(target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if(dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.position += dir.normalized * distanceThisFrame;
        }

        void HitTarget()
        {
            GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);

            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}