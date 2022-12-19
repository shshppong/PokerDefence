using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    // public enum UnitType
    // {
    //     None,
    //     GWANG_TTAENG,       // 광땡
    //     TTAENG,             // 땡
    //     ALLI,               // 알리
    //     DOKSA,              // 독사
    //     GOO_BING,           // 구삥
    //     JANG_BING,          // 장삥
    //     JANG_SA,            // 장사
    //     SEL_YUG,            // 세륙
    //     GAB_O,              // 갑오
    //     TTAENG_COUNTER,     // 떙잡이
    //     GU_SA,              // 구사
    //     FISH_GU_SA,         // 멍텅구리 구사
    //     SECRET_ROYAL_PALACE,// 암행어사
    //     KKEUS,              // 끗
    //     MANG_TONG           // 망통
    // }
    
    [CreateAssetMenu(fileName = "Unit", menuName = "Unit/Unit")]

    public class UnitData : ScriptableObject
    {
        public string unitName;
        public int attack;
        public float attackSpeed;
        public float attackRange;
        public float moveSpeed;

        public GameObject unitModel;
        public GameObject bulletModel;
    }
}
