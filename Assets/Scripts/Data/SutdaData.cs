using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    public class SutdaData : MonoBehaviour
    {
        [SerializeField]
        private CardData cardData;
        public CardData CardData { get { return cardData; } }
        
        public Sprite GetSprite(int index)
        {
            Sprite image = cardData.data[0].sprites;

            foreach(Data id in cardData.data)
            {
                if(id.ID == index)
                {
                    return id.sprites;
                }
            }

            return image;
        }
    }

}
