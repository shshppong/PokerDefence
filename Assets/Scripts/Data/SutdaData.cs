using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HwatuDefence
{
    public class SutdaData : MonoBehaviour
    {
        [SerializeField]
        private CardData cardData;
        public CardData CardData { get { return cardData; } }

        public static Action target;

        private int index = 0;

        private void Awake() {
            target = () =>
            {
                CardInfo(this.index);
            };
        }

        public void CardInfo(int index)
        {
            Debug.Log("카드 숫자 :: " + cardData.Cards[index].CardNumber);
            Debug.Log("카드 숫자 :: " + cardData.Cards[index].Description);
            
            for(int i = 0; i < 2; i++)
            {
                Debug.Log("카드 속성 :: " + cardData.Cards[index].Sprites[i]);
            }
        }
    }
}
