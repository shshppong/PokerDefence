using UnityEngine;

namespace HwatuDefence
{
    [System.Serializable]
    public class UnitInfo
    {
        [HideInInspector]
        public string elementName;
        public string unitName;
        public int attack;
        public float attackSpeed;
        public float attackRange;
        public float moveSpeed;
    }
}