using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card/Card", order = int.MaxValue)]

    public class CardData : ScriptableObject
    {
        [Serializable]
        public class Card
        {
            [SerializeField]
            private string cardNumber;
            public string CardNumber { get { return cardNumber; } }

            [SerializeField]
            private string description;
            public string Description { get { return description; } }

            [SerializeField]
            private List<Sprite> sprites = new List<Sprite>();
            public List<Sprite> Sprites { get { return sprites; } }
        }

        [SerializeField]
        private Card[] cards;
        public Card[] Cards { get { return cards; } }
    }
}
