using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class PickUpEvent : MonoBehaviour
    {
        
        public static int[]          random;                // 섯다 카드 랜덤에 사용하는 변수 

        public SutdaData             data;

        public GameObject            pickUpPanel;

        [SerializeField]
        private Image[]              CardsUI;
        [SerializeField]
        private Text[]               CardsTextUI;
        
        [SerializeField]
        private Button[]             reRollBtn;

        [SerializeField]
        private	UnitSpawner			 unitSpawner;

        [SerializeField]
	    private	RTSUnitController	 rtsUnitController;

        private bool[]               selectCardBool;         // 카드 Image 선택시 선택 중인지 아닌지 사용

        [SerializeField]
        private Text                reRollCountText;

        [SerializeField]
        private int                 reRollCount = 2;            // 리롤 카운트

        void Start()
        {
            random = new int [2];
            selectCardBool = new bool[2];
            for(int i = 0; i < 2; i++)
            {
                selectCardBool[i] = false;
                reRollBtn[i].interactable = false;
            }

            // 처음에는 비활성화
            pickUpPanel.SetActive(false);
        }

        void Init()
        {
            for(int i = 0; i < 2; i++)
            {
                selectCardBool[i] = false;
                reRollBtn[i].interactable = false;
                CardsUI[i].color = new Color32(255, 255, 255, 255);
            }
            reRollCountText.text = "개별 리롤 횟수 : " + reRollCount.ToString();
        }

        // 호출되면 카드 뽑기
        public void PickUpMain()
        {
            Init();
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
            // 개별 리롤 횟수 카운트가 1 이상이면 선택 할 수 있도록 하기
            if(reRollCount < 1)
            {
                // 남은 횟수 빨갛게 보여주다가 다시 돌아오기 (애니메이션)
                StartCoroutine(ChangeTextToZeroColor(0.5f, reRollCountText));
                StartCoroutine(ChangeTextToFullColor(0.5f, reRollCountText));
                return;
            }
            if(selectCardBool[0] || selectCardBool[1])
                reRollBtn[0].interactable = false;
            else
                reRollBtn[0].interactable = true;

            if(!selectCardBool[value])
            {
                // 카드를 선택 했을 때
                selectCardBool[value] = true;
                CardsUI[value].color = new Color32(255, 255, 255, 10);
            }
            else
            {
                // 카드 선택 해제 됐을 때
                selectCardBool[value] = false;
                CardsUI[value].color = new Color32(255, 255, 255, 255);
            }
        }

        public IEnumerator ChangeTextToFullColor(float t, Text i)
        {
            i.color = new Color(0, i.color.g, i.color.b, i.color.a);
            while (i.color.r < 1.0f)
            {
                // 요거 수정 R 만 했는데 GB 를 수정해야함
                i.color = new Color(i.color.r + (Time.deltaTime / t), i.color.g, i.color.b, i.color.a);
                yield return null;
            }
        }
    
        public IEnumerator ChangeTextToZeroColor(float t, Text i)
        {
            i.color = new Color(1, i.color.g, i.color.b, i.color.a);
            while (i.color.r > 0.0f)
            {
                i.color = new Color(i.color.r + (Time.deltaTime / t), i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }

        public void ChangeCard()
        {
            // 카드 개별 리롤 하기 ( 전체 리롤도 여기서 처리하게 만들기 )
        }

        // 선택
        public void ClickApply()
        {
            rtsUnitController.AddRangeUnitList(unitSpawner.SpawnUnits(1));
            // 적용 될 때 카드 조합 데이터에서 공격력을 가져오고 
            // 소환 될 유닛에다가 해당 데이터를 집어넣어서 소환시킨다.

            pickUpPanel.SetActive(false);
        }
        
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

    }
}
