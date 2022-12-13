using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class PickUpEvent : MonoBehaviour
    {
        // 섯다 카드 랜덤에 사용하는 변수 
        public static int[]          random;

        public SutdaData             data;

        public GameObject            pickUpPanel;

        [SerializeField]
        private Image[]              CardsUI;
        [SerializeField]
        private Text[]               CardsTextUI;

        [SerializeField]
        private	UnitSpawner			 unitSpawner;

        [SerializeField]
	    private	RTSUnitController	rtsUnitController;

        // 중복되지 않는 랜덤 함수
        // 0 ~ 19의 숫자 중 랜덤으로 2번 뽑는데, 중복 되지 않아야 한다.
        public int[] GetRandomInt(int length, int min, int max)
        {
            int[] randArray = new int[length];
            bool isSame;

            for(int i = 0; i < length; ++i)
            {
                while(true)
                {
                    randArray[i] = Random.Range(min, max);
                    isSame = false;

                    for(int j = 0; j < i; ++j)
                    {
                        if(randArray[j] == randArray[i])
                        {
                            isSame = true;
                            break;
                        }
                    }
                    if(!isSame) break;
                }
            }
            return randArray;
        }

        void Start()
        {
            random = new int [2];

            // 처음에는 비활성화
            pickUpPanel.SetActive(false);
        }

        // 호출되면 카드 뽑기
        public void PickUpMain()
        {
            pickUpPanel.SetActive(true);
            // 이미지 데이터를 불러오고 랜덤 값을 추출한 다음 
            // 랜덤 값에 맞게 이미지를 정렬하고 UI로 보여주기
            random = GetRandomInt(2, 0, 20);
            CardsUI[0].sprite = data.GetSprite(random[0]);
            CardsUI[1].sprite = data.GetSprite(random[1]);

            CardsTextUI[0].text = data.GetSprite(random[0]).name.ToString();
            CardsTextUI[1].text = data.GetSprite(random[1]).name.ToString();
        }

        public void SelectCard(int value)
        {
            // 카드를 선택 했을 때
            if(value == 0)
            {
                Debug.Log(CardsUI[0].sprite.name.ToString());
            }

            if(value == 1)
            {
                Debug.Log(CardsUI[1].sprite.name.ToString());
            }
        }

        // 선택
        public void ClickApply()
        {
            rtsUnitController.AddRangeUnitList(unitSpawner.SpawnUnits(1));
            // 적용 될 때 카드 조합 데이터에서 공격력을 가져오고 
            // 소환 될 유닛에다가 해당 데이터를 집어넣어서 소환시킨다.

            pickUpPanel.SetActive(false);
        }
    }
}
