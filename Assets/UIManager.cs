using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    public class UIManager : MonoBehaviour
    {
        public void OnButtonDown_SelectUnit()
        {
            // 유닛 뽑기 - 섯다 카드로 유닛의 공격력 정하기
            // 이 버튼을 눌렀을 때, PickUpPanel이 열리게 하기

            SutdaData.target();
        }
    }
}
