using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card/Card", order = int.MaxValue)]

    public class CardData : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();
        public List<Sprite> Sprites { get { return sprites; } }
    }
}
