using UnityEngine;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Unit/Unit", order = int.MaxValue)]

    public class UnitData : ScriptableObject
    {
        [SerializeField]
        private string unitName;
        public string UnitName { get { return unitName; } }
        
        /*
        [SerializeField]
        private int hp;
        public int HP { get { return hp; } }
        */

        [SerializeField]
        private int damage;
        public int Damage { get { return damage; } }

        [SerializeField]
        private float moveSpeed;
        public float MoveSpeed { get { return moveSpeed; } }
    }
}

/*
namespace PokerDefence
{
    enum StatusType
    {
        None,
        HP,
    }

    enum CardNum
    {
        SEVEN,              // 0
        EIGHT,              // 1
        NINE,               // 2
        TEN,                // 3
        KNIGHT,             // 4
        QUEEN,              // 5
        KING,               // 6
        None,
        ACE                 // 8
    }

    public enum PairType
    {
        QUEEN_TOP,
        TOP,
        ONE,
        TWO,
        TRIPLE,
        FULL_HOUSE,
        STRAIGHT,
        FOUR_CARD,
        BACK_STRAIGHT,
        MOUNTAIN,
        FLUSH,
        FIVE_CARD,
        BACK_STRAIGHT_FLUSH,
        FIVE_FLUSH,
        ROYAL_STRAIGHT_FLUSH
    }

    class UnitData
    {
        PairType type;
        protected string name = null;
        protected int hp = 0;

        // 클래스 기본 선언
        protected UnitData(PairType type)
        {
            this.type = type;
        }

        public void SetInfo(int hp)
        {
            this.hp = hp;
        }

        public void AddInfo(StatusType type, int value)
        {
            if(type.Equals(StatusType.HP))
            {
                hp += value;
            }
        }

        public int GetInfo(StatusType type)
        {
            if(! type.Equals(StatusType.HP))
            {
                return 0;
            }
            int value = hp;
            return value;
        }
    }
}
*/
