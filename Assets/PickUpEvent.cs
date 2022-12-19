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

        private bool isAllReRoll;

        [SerializeField]
        private Text cardHelperText;
        [SerializeField]
        private Animation cardHelperAnim;

        private UnitType type;
        private int cardNum;

        void Start()
        {
            random = new int [2];
            selectCardBool = new bool[2];
            for(int i = 0; i < 2; i++)
            {
                selectCardBool[i] = false;
                reRollBtn[i].interactable = false;
            }
            isAllReRoll = false;

            // 처음에는 비활성화
            pickUpPanel.SetActive(false);
        }

        void Init()
        {
            for(int i = 0; i < 2; i++)
            {
                selectCardBool[i] = false;
                CardsUI[i].color = new Color32(255, 255, 255, 255);
            }
            reRollBtn[0].interactable = false;
            reRollBtn[1].interactable = true;
            reRollCountText.text = "개별 리롤 횟수 : " + reRollCount.ToString();

            type = UnitType.None;
            cardNum = 0;
        }

        // 호출되면 카드 뽑기
        public void PickUpMain()
        {
            Init();
            GenerateCards();
            pickUpPanel.SetActive(true);
        }
        // 2개 카드 리롤
        private void GenerateCards()
        {
            // 이미지 데이터를 불러오고 랜덤 값을 추출한 다음 
            // 랜덤 값에 맞게 이미지를 정렬하고 UI로 보여주기
            random = GetRandomInt(2, 1, 21);
            for(int i = 0; i < CardsUI.Length; i++)
            {
                CardsUI[i].sprite = data.GetSprite(random[i]);
                // CardsTextUI[i].text = data.GetSprite(random[i]).name.ToString();
                CardsTextUI[i].text = (random[i] % 10 == 0 ? 10 : random[i] % 10).ToString();
            }
            CheckJokbo(random);
            cardHelperAnim.Play();
        }
        // 1개만 카드 리롤
        private void GenerateCards(int index)
        {
            // 1개만 카드 리롤
            random = GetRandomIntOnce(random, index, 1, 21);
            CardsUI[index].sprite = data.GetSprite(random[index]);
            CardsTextUI[index].text = (random[index] % 10 == 0 ? 10 : random[index] % 10).ToString();
            CheckJokbo(random);
            cardHelperAnim.Play();
        }
        // 카드 선택 시 상호작용
        public void SelectCard(int value)
        {
            // 개별 리롤 횟수 카운트가 1 이상이면 선택 할 수 있도록 하기
            if(reRollCount < 1)
            {
                // 남은 횟수 빨갛게 보여주다가 다시 돌아오기 (애니메이션)
                StartCoroutine(ChangeTextRedToWhiteColor(0.5f, reRollCountText));
                return;
            }
            ChangeSelectCardColor(value);
            CheckSelectCardInteractable();
        }
        private void ChangeSelectCardColor(int value)
        {
            if(!selectCardBool[value])
            {
                // reRollBtn 버튼이 활성화 되었을 시에, 카드 선택이 되지 않도록 하기 (해제만 되게 하기)
                if(reRollBtn[0].interactable) return;

                // 카드를 선택 했을 때
                selectCardBool[value] = true;
                CardsUI[value].color = new Color32(255, 255, 255, 10);

                // 선택되면 다시 뽑기 버튼 비활성화
                if(!isAllReRoll)
                    reRollBtn[1].interactable = false;
            }
            else
            {
                // 카드 선택 해제 됐을 때
                selectCardBool[value] = false;
                CardsUI[value].color = new Color32(255, 255, 255, 255);

                // 선택되면 다시 뽑기 버튼 활성화
                if(!isAllReRoll)
                    reRollBtn[1].interactable = true;
            }
        }
        private void CheckSelectCardInteractable()
        {
            if(selectCardBool[0] || selectCardBool[1])
            {
                reRollBtn[0].interactable = true;
            }
            else
            {
                reRollBtn[0].interactable = false;
            }
            
            if(isAllReRoll) reRollBtn[1].interactable = false;
        }
        // 선택 리롤
        public void ChangeSelectCard()
        {
            // 카드 개별 리롤 하기 ( 전체 리롤도 여기서 처리하게 만들기 )
            for(int i = 0; i < CardsUI.Length; i++)
            {
                if(selectCardBool[i])
                {
                    GenerateCards(i);
                    reRollCount--;
                    reRollCountText.text = "개별 리롤 횟수 : " + reRollCount.ToString();

                    selectCardBool[i] = false;
                    CardsUI[i].color = new Color32(255, 255, 255, 255);
                    reRollBtn[0].interactable = false;
                    CheckSelectCardInteractable();
                }
            }
        }
        // 전부 리롤
        public void ChangeCardAll()
        {
            GenerateCards();
            isAllReRoll = true;
            CheckSelectCardInteractable();
        }

        public IEnumerator ChangeTextRedToWhiteColor(float t, Text i)
        {
            i.color = new Color(i.color.r, 0, 0, i.color.a);
            while (i.color.g < 1.0f)
            {
                // 요거 수정 R 만 했는데 GB 를 수정해야함
                i.color = new Color(i.color.r, 
                                    i.color.g + (Time.deltaTime / t), 
                                    i.color.b + (Time.deltaTime / t), 
                                    i.color.a);
                yield return null;
            }
        }

        // 선택
        public void ClickApply()
        {
            // rtsUnitController.AddRangeUnitList(unitSpawner.SpawnUnits(1));
            // 적용 될 때 카드 조합 데이터에서 공격력을 가져오고 
            // 소환 될 유닛에다가 해당 데이터를 집어넣어서 소환시킨다.
            unitSpawner.SpawnUnits(type, cardNum);
            type = UnitType.None;
            cardNum = 0;
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

        // 1개만 중복되지 않는 랜덤 적용하기
        public int[] GetRandomIntOnce(int[] arr, int index, int min, int max)
        {
            int getAnotherIndex = (index % 2 == 0) ? 1 : 0;
            int sameNum = arr[getAnotherIndex];
            int rand;
            bool isSame;

            while(true)
            {
                rand = Random.Range(min, max);
                isSame = false;

                if(rand == sameNum)
                {
                    isSame = true;
                    continue;
                }
                if(!isSame) break;
            }
            arr[index] = rand;
            return arr;
        }

        public void CheckJokbo(int[] arr)
        {
            // 원본 값 OV (Original Value)
            int[] OV = arr;

            // 연산 D(Double) T(Try)
            int DT = arr[0] % 10 == 0 ? (arr[0]==10 ? 10 : 10) : arr[0] % 10;
            int D = DT==0 ? 2 : DT;
            int DDT = arr[1] % 10 == 0 ? (arr[0]==10 ? 10 : 10) : arr[1] % 10;
            int DD = DDT==0 ? 2 : DDT;

            int add = D + DD;

            // 끗 (a+b) % 10 = x, x <= 8
            int z = add > 10 ? add%10 : add;


            // 처리

            // 특수 기능 족보
            // 땡잡이
            if((D == 3 || DD == 3) && (D == 7 || DD == 7))
            {
                cardHelperText.text = "땡잡이".ToString();
                type = UnitType.TTAENG_COUNTER;
                return;
            }

            // 구사
            if((D == 4 || DD == 4) && (D == 9 || DD == 9))
            {
                cardHelperText.text = "구사".ToString();
                // 다시 뽑기
                return;
            }

            // 멍텅구리 구사
            if((OV[0] == 4 || OV[1] == 4) && (OV[0] == 9 || OV[1] == 9))
            {
                cardHelperText.text = "멍텅구리 구사".ToString();
                // 다시 뽑기
                return;
            }

            // 암행어사
            if((OV[0] == 4 || OV[1] == 4) && (OV[0] == 7 || OV[1] == 7))
            {
                cardHelperText.text = "암행어사".ToString();
                type = UnitType.SECRET_ROYAL_PALACE;
                return;
            }

            // 광땡
            if(OV[0] == 1 || OV[0] == 3 || OV[0] == 8) // 0번 패가 일광, 삼광, 팔광이 있을 때
            {
                if(OV[0] == 1 || OV[0] == 3 || OV[0] == 8) // 1번 패에도 있을 때
                {
                    int total = OV[0] + OV[1];
                    switch(total)
                    {
                        case 11:
                            cardHelperText.text = "삼팔광땡".ToString();
                            type = UnitType.GWANG_TTAENG38;
                            return;
                        case 9:
                            cardHelperText.text = "일팔광땡".ToString();
                            type = UnitType.GWANG_TTAENG18;
                            return;
                        case 4:
                            cardHelperText.text = "일삼광땡".ToString();
                            type = UnitType.GWANG_TTAENG13;
                            return;
                    }
                }
            }

            // 땡
            if(D == DD) // 숫자가 같으면 땡
            {
                if(D == 10 && DD == 10) // 둘 다 10이면
                {
                    cardHelperText.text = "장땡".ToString();
                    type = UnitType.TTAENG;
                    return;
                }
                if(D == 1 && DD == 1)
                {
                    cardHelperText.text = "삥땡".ToString();
                    type = UnitType.TTAENG;
                    return;
                }
                cardHelperText.text = D + "땡".ToString();
                type = UnitType.TTAENG;
                cardNum = D;
                return;
            }

            // 알리
            if((D == 1 || DD == 1) && (D == 2 || DD == 2))
            {
                cardHelperText.text = "알리".ToString();
                type = UnitType.AL_LI;
                return;
            }

            // 독사
            if((D == 1 || DD == 1) && (D == 4 || DD == 4))
            {
                cardHelperText.text = "독사".ToString();
                type = UnitType.DOK_SA;
                return;
            }

            // 구삥     순서와 상관없이 1월 9월 조합
            if((D == 1 || DD == 1) && (D == 9 || DD == 9)) // D가 1이고 DD가 9일때, 또는 D가 9이고 DD가 1일때
            {
                cardHelperText.text = "구삥".ToString();
                type = UnitType.KKERUS9;
                return;
            }
            // 장삥
            if((D == 1 || DD == 1) && (D == 10 || DD == 10))
            {
                cardHelperText.text = "장삥".ToString();
                type = UnitType.KKERUS10;
                return;
            }

            // 갑오 (9끗이 갑오)
            if(add % 10 == 9)
            {
                cardHelperText.text = "갑오".ToString();
                type = UnitType.GAB_O;
                return;
            }

            // 망동
            if(add % 10 == 0)
            {
                cardHelperText.text = "망통".ToString();
                type = UnitType.MANG_TONG;
                return;
            }

            // 끗
            if(z <= 8 && z >= 1)
            {
                cardHelperText.text = z + "끗".ToString();
                type = UnitType.KKEUS;
                cardNum = z;
                return;
            }
        }

        // 치트
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GenerateCards();
            }
        }
    }
}