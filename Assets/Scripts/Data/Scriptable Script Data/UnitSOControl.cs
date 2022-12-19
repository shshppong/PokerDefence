using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HwatuDefence
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitSOControl : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        private float wanderDistance = 3;

        public UnitData data;

        private void Start()
        {
            if(navAgent == null)
                navAgent = this.GetComponent<NavMeshAgent>();
            
            if(data != null)
                LoadEnemy(data);
        }

        private void LoadEnemy(UnitData _data)
        {
            // 자식 오브젝트 삭제
            foreach(Transform child in this.transform)
            {
                if(Application.isEditor)
                    DestroyImmediate(child.gameObject);
                else
                    Destroy(child.gameObject);
            }

            // 적 요소 불러오기
            GameObject visuals = Instantiate(data.unitModel);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = Vector3.zero;
            visuals.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            // 유닛의 스텟 데이터
            if(navAgent == null)
                navAgent = this.GetComponent<NavMeshAgent>();
            
            this.navAgent.speed = data.moveSpeed;
        }

        private void Update()
        {
            if(data == null) return;

            if(navAgent.remainingDistance < 1f)
                GetNewDestination();
        }

        private void GetNewDestination()
        {
            Vector3 nextDestination = this.transform.position;
            nextDestination += wanderDistance * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
            
            NavMeshHit hit;
            if(NavMesh.SamplePosition(nextDestination, out hit, 3f, NavMesh.AllAreas))
                navAgent.SetDestination(hit.position);
        }
    }

}