using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int hp;
        public float moveSpeed;

        public GameObject enemyModel;
    }
}
