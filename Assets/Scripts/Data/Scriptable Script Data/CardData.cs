using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    [System.Serializable]
    public class Data
    {
        [HideInInspector]
        public string elementName;
        public int ID;
        public string itemName;
        public Sprite sprites;
    }

    [CreateAssetMenu(fileName = "Card", menuName = "Card/Card")]

    public class CardData : ScriptableObject
    {
        [Header("이미지 데이터")]
        public Sprite[] imageData;

        [Header("실제 처리 데이터")]
        public List<Data> data;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < data.Count; i++)
            {
                data[i].elementName = "[" + (i) + "] \t" + data[i].itemName;
                data[i].ID = i;
                data[i].sprites = imageData[i];
            }
        }
#endif
        // 작업 완료 지워도 됨
        // [Header("임시 데이터")]
        // [SerializeField]
        // private List<Sprite> sprites = new List<Sprite>();
        // public List<Sprite> Sprites { get { return sprites; } }
    }
    
}
