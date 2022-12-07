using UnityEngine;

namespace PokerDefence
{
    public class Bullet : MonoBehaviour
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

            target.gameObject.SetActive(false); // Destory(target.gameObject);
            Destroy(gameObject); // 탄환 오브젝트는 즉시 제거
        }
    }
}